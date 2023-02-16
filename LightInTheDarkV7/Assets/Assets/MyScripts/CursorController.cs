using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    Vector2 Movement;
    public float Speed;
    public bool IsPressed;
    private Rigidbody2D RB;

    IEnumerator StopPress()
    {
        yield return new WaitForSeconds(1f);
        IsPressed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector2(Movement.x, Movement.y) * Speed * Time.deltaTime;

        if (IsPressed)
        {
            StartCoroutine(StopPress());
        }

        Time.timeScale = 1;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Movement = ctx.ReadValue<Vector2>();
    }

    public void OnPress(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            IsPressed = true;
        }
    }

    public void OnDelete(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPressed && collision.gameObject.tag == "Play")
        {
            SceneManager.LoadScene(1);
        }
        else if (IsPressed && collision.gameObject.tag == "Options")
        {
            //SceneManager.LoadScene(1); Come up with an option menu
            SceneManager.LoadScene("Options");
        }
        else if (IsPressed && collision.gameObject.tag == "Quit")
        {
            
            Application.Quit();
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsPressed && collision.gameObject.tag == "Play")
        {
            SceneManager.LoadScene(1);
        }
        else if (IsPressed && collision.gameObject.tag == "Options")
        {
            //SceneManager.LoadScene(1); Come up with an option menu
            SceneManager.LoadScene("Options");
        }
        else if (IsPressed && collision.gameObject.tag == "Quit")
        {

            Application.Quit();
        }
    }
}
