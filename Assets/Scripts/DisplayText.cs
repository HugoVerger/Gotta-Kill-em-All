using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public GameObject textBubble;
    public Text text;

    public void displayText(string newText)
    {
        textBubble.SetActive(true);
        text.text = newText;
    }
}
