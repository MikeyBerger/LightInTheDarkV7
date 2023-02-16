using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV2 : MonoBehaviour
{
    Vector2 PlayerMovement;
    Vector2 LightMovement;
    public float PlayerSpeed;
    public float LightSpeed;
    public float DashForce;
    public float DropTimer;
    private float Direction = 1;
    public bool IsDashing;
    public bool IsGrounded;
    private bool FacingRight = true;
    private Rigidbody2D RB;
    private Transform Light;

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(DropTimer);
        IsDashing = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Light = GameObject.FindGameObjectWithTag("Light").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Direction = PlayerMovement;
        //RB.velocity = new Vector2(PlayerMovement.x * PlayerSpeed, PlayerMovement.y * PlayerSpeed) * Time.deltaTime;
        //transform.Translate(new Vector3(PlayerMovement.x, PlayerMovement.y, 0) * PlayerSpeed * Time.deltaTime);
        RB.MovePosition(RB.position + PlayerMovement * PlayerSpeed * Time.deltaTime);
        Light.transform.Translate(new Vector2(LightMovement.x, LightMovement.y) * LightSpeed * Time.deltaTime);


        Dash();
        Flip();
    }

    void Dash()
    {
        if (IsDashing && PlayerMovement.x != 0 || PlayerMovement.y != 0)
        {
            RB.AddForce(new Vector2(PlayerMovement.normalized.x, PlayerMovement.normalized.y) * DashForce, ForceMode2D.Impulse);
            
            //StartCoroutine(StopJump());
            //transform.Translate(Vector3.up * JumpForce);
            IsDashing = false;
        }
        else if (IsDashing && !FacingRight && PlayerMovement.x == 0 && PlayerMovement.y == 0)
        {
            RB.AddForce(Vector2.left * DashForce, ForceMode2D.Impulse);

            IsDashing = false;
        }
        else if (IsDashing && FacingRight && PlayerMovement.x == 0 && PlayerMovement.y == 0)
        {
            RB.AddForce(Vector2.right * DashForce, ForceMode2D.Impulse);

            IsDashing = false;
        }



    }

    void AnimatePlayer()
    {

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
            //RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = false;
        }
    }
}
