using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    public float MinSize;
    public float MaxSize;
    private float RandSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RandSize = Random.Range(MinSize, MaxSize);

        transform.localScale = new Vector3(RandSize, RandSize, 0);
    }
}
