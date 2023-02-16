using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Vector2 PlayerMovement;
    Vector2 LightMovement;
    public float PlayerSpeed;
    public float LightSpeed;
    public float DashForce;
    public float LadderSpeed;
    public float DashTimer;
    public float DropSpeed;
    public bool IsPaused;
    public bool IsGrounded;
    public bool AirBorn;
    public bool IsDashing;
    public bool FacingRight = true;
    private Transform LightObject;
    private Rigidbody2D RB;
    private Animator Anim;
    public Canvas PauseCanvas;

    IEnumerator StopDash()
    {
        //transform.position = Vector3.zero;
        yield return new WaitForSeconds(DashTimer);
        IsDashing = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        LightObject = GameObject.FindGameObjectWithTag("Light").GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        //PauseCanvas.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * PlayerSpeed * Time.deltaTime;
        LightObject.transform.Translate(new Vector2(LightMovement.x, LightMovement.y) * LightSpeed * Time.deltaTime);


        if (IsDashing)
        {
            if (FacingRight)
            {
                RB.AddForce(Vector2.right * DashForce * Time.deltaTime);
                RB.velocity = Vector2.zero;
                //IsDashing = false;
                StartCoroutine(StopDash());
            }
            else if (!FacingRight)
            {
                RB.AddForce(Vector2.left * DashForce * Time.deltaTime);
                RB.velocity = Vector2.zero;
                //IsDashing = false;
                StartCoroutine(StopDash());
            }

        }

        if (!IsDashing)
        {
            RB.velocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * PlayerSpeed * Time.deltaTime;
        }

        Flip();
        AnimatePlayer();
        //Dash();
        //Drop();
        Pause();
    }

    void Flip()
    {
        Vector3 Scale = transform.localScale;

        if (PlayerMovement.x > 0 && !FacingRight || PlayerMovement.x < 0 && FacingRight)
        {
            FacingRight = !FacingRight;
            Scale.x *= -1;
        }

        transform.localScale = Scale;
    }
    void AnimatePlayer()
    {
        if (PlayerMovement.x > 0 || PlayerMovement.x < 0)
        {
            Anim.SetBool("IsRunning", true);
        }
        else
        {
            Anim.SetBool("IsRunning", false);
        }

        if (IsGrounded == false)
        {
            Anim.SetBool("Grounded", false);
        }
        else if (IsGrounded == true)
        {
            Anim.SetBool("Grounded", true);
        }
    }
    void Dash()
    {
        //RB.AddForce(Vector2.up * JumpForce);
        //IsJumping = true;

        if (IsDashing)
        {
            if (FacingRight)
            {
                RB.AddForce(Vector2.right * DashForce * Time.deltaTime);
                RB.velocity = Vector2.zero;
            }
            else if (!FacingRight)
            {
                RB.AddForce(Vector2.left * DashForce * Time.deltaTime);
                RB.velocity = Vector2.zero;
            }

        }
        //IsDashing = false;
        StartCoroutine(StopDash());
    }

    void Drop()
    {
        if (AirBorn)
        {
            transform.Translate(0, DropSpeed * Time.deltaTime, 0);
            //RB.gravityScale = RB.gravityScale * DropSpeed * Time.deltaTime;
        }
    }

    void Pause()
    {
        if (IsPaused)
        {
            Time.timeScale = 0;
            PauseCanvas.enabled = true;

        }
        else if (!IsPaused)
        {
            Time.timeScale = 1;
            PauseCanvas.enabled = false;
        }
    }

    #region InputActions
    public void OnMovePlayer(InputAction.CallbackContext ctx)
    {
        PlayerMovement = ctx.ReadValue<Vector2>();
    }

    public void OnMoveLight(InputAction.CallbackContext ctx)
    {
        LightMovement = ctx.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            IsDashing = true;
        }
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed && !IsPaused)
        {
            IsPaused = true;

        }
        else if (ctx.phase == InputActionPhase.Performed && IsPaused)
        {
            IsPaused = false;
        }
    }

    public void OnReturnToMenu(InputAction.CallbackContext ctx)
    {
        if (IsPaused && ctx.phase == InputActionPhase.Performed)
        {
            SceneManager.LoadScene(0);
            
        }
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "GroundTilemap")
        {
            IsGrounded = false;
            AirBorn = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.name == "GroundTilemap")
        {
            IsGrounded = true;
            AirBorn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder" && PlayerMovement.y == 0)
        {
            Anim.SetBool("IsStill", true);
            Anim.SetBool("IsClimbing", false);
        }
        else if (collision.gameObject.tag == "Ladder" && PlayerMovement.y >= 0 || PlayerMovement.y <= 0)
        {
            Anim.SetBool("IsStill", false);
            Anim.SetBool("IsClimbing", true);
            RB.velocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * LadderSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            Anim.SetBool("IsStill", false);
            Anim.SetBool("IsClimbing", false);
        }
    }
    #endregion
}
