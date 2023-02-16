using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D RB;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Up")
        {
            RB.velocity = new Vector2(Speed, 0);
        }

        if (collision.gameObject.tag == "Down")
        {
            RB.velocity = new Vector2(Speed, 0) * -1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Up")
        {
            RB.velocity = new Vector2(Speed, 0);
        }

        if (collision.gameObject.tag == "Down")
        {
            RB.velocity = new Vector2(Speed, 0) * -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Up")
        {
            RB.velocity = new Vector2(Speed, 0);
        }

        if (collision.gameObject.tag == "Down")
        {
            RB.velocity = new Vector2(Speed, 0) * -1;
        }
    }
}
