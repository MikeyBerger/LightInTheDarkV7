using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerScript : MonoBehaviour
{
    int ThisScene;

    // Start is called before the first frame update
    void Start()
    {
        ThisScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            //Add Some kind of death VFX and SFX
            SceneManager.LoadScene(ThisScene);
            
        }
    }

    
}
