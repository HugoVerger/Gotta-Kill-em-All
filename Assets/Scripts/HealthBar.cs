using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite health4;
    public Sprite health3;
    public Sprite health2;
    public Sprite health1;
    public Sprite health0;
    Image image;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void UpdateHealthBar(int currentHealth)
    {
        if (currentHealth == 4)
        {
            image.sprite = health4;
        }
        else if (currentHealth == 3)
        {
            image.sprite = health3;
        }
        else if (currentHealth == 2)
        {
            image.sprite = health2;
        }
        else if (currentHealth == 1)
        {
            image.sprite = health1;
        }
        else
        {
            image.sprite = health0;
        }
    }
}
