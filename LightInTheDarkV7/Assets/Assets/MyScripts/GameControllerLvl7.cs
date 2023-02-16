using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameControllerLvl7 : MonoBehaviour
{
    public TilemapRenderer DangerRenderer1;
    public TilemapCollider2D DangerCollider1;
    public float WaitTimer;
    public float Limit;
    public float EnableTimer;
        

    IEnumerator Reenable()
    {
        yield return new WaitForSeconds(EnableTimer);
        WaitTimer = 0;
        DangerRenderer1.enabled = true;
        DangerCollider1.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        DangerRenderer1.enabled = true;
        DangerCollider1.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        WaitTimer += Time.deltaTime;

        if (WaitTimer >= Limit)
        {
            DangerRenderer1.enabled = false;
            DangerCollider1.enabled = false;
            StartCoroutine(Reenable());
        }
    }
}
