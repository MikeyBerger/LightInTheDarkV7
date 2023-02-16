using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI TextDisplay;
    public TextMeshProUGUI ContinueDisplay;
    public string[] Sentences;
    public int Index;
    public float TypingSpeed;

    //private float PlayCutscene = PlayerPrefs.GetInt("PlayCutScene", 1);

    IEnumerator PressA()
    {
        yield return new WaitForSeconds(1f);
        ContinueDisplay.text = "Press A";
    }

    IEnumerator Type()
    {
        foreach (char letter in Sentences[Index].ToCharArray())
        {
            TextDisplay.text += letter;
            
            yield return new WaitForSeconds(TypingSpeed);
            //StartCoroutine(PressA());
        }
        

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
        ContinueDisplay.text = "Press A to continue";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextSentence()
    {
        if (Index < Sentences.Length - 1)
        {
            Index++;
            TextDisplay.text = "";
            StartCoroutine(Type());
        }
    }



    public void OnContinue(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            NextSentence();
        }

        if (Index == 2 && ctx.phase == InputActionPhase.Performed)
        {
            SceneManager.LoadScene(2);
        }
    }
}
