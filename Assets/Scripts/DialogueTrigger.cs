using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public string text1 = "";
    public string text2 = "";
    public string text3 = "";
    public string text4 = "";
    public bool hasToPressEnter = false;
    Image textBubble;
    Text textDialogue;
    bool isPlayerInside;
    int currentText;

    // Use this for initialization
    void Start()
    {
        textBubble = GameObject.Find("TextBubble").GetComponent<Image>();
        textDialogue = GameObject.Find("TextDialogue").GetComponent<Text>();
        isPlayerInside = false;
        currentText = 1;
    }

    void Update()
    {
        if (isPlayerInside)
        {
            if (textBubble.enabled == false)
            {
                if (hasToPressEnter)
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        textBubble.enabled = true;
                        textDialogue.text = text1;
                    }
                }
                else
                {
                    textBubble.enabled = true;
                    textDialogue.text = text1;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (currentText == 1 && text2 != "")
                    {
                        currentText = 2;
                        textDialogue.text = text2;
                    }
                    else if (currentText == 2 && text3 != "")
                    {
                        currentText = 3;
                        textDialogue.text = text3;
                    }
                    else if (currentText == 3 && text4 != "")
                    {
                        currentText = 4;
                        textDialogue.text = text4;
                    }
                    else
                    {
                        isPlayerInside = false;
                        textBubble.enabled = false;
                        textDialogue.text = "";
                        currentText = 1;

                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isPlayerInside = false;
            textBubble.enabled = false;
            textDialogue.text = "";
            currentText = 1;
        }
    }
}
