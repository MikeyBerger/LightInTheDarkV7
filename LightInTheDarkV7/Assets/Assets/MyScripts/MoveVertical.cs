using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVertical : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D RB;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Up")
        {
            //transform.Translate(0, Speed, 0);
            RB.velocity = new Vector2(0, Speed);
        }

        if (collision.gameObject.tag == "Down")
        {
            //transform.Translate(0, Speed * -1, 0);
            RB.velocity = new Vector2(0, Speed) * -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Up")
        {
            //transform.Translate(0, Speed, 0);
            RB.velocity = new Vector2(0, Speed);
        }

        if (collision.gameObject.tag == "Down")
        {
            //transform.Translate(0, Speed * -1, 0);
            RB.velocity = new Vector2(0, Speed) * -1;
        }
    }
}
