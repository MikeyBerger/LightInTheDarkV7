using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool IsOrange;
    public float Distance;
    private Transform Destination;
    private Transform Light;

    // Start is called before the first frame update
    void Start()
    {
        Light = GameObject.FindGameObjectWithTag("Light").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOrange)
        {
            Destination = GameObject.FindGameObjectWithTag("OrangePortal").GetComponent<Transform>();
        }
        else
        {
            Destination = GameObject.FindGameObjectWithTag("YellowPortal").GetComponent<Transform>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Vector2.Distance(transform.position, collision.transform.position) > Distance)
        {
            collision.transform.position = new Vector2(Destination.position.x, Destination.position.y);
            Light.transform.position = new Vector2(Destination.position.x, Destination.position.y);
        }
    }
}
