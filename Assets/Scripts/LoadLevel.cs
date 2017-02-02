using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string nom_scene;
    public float delay = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameObject.Find("BlackScreen").GetComponent<Animator>().Play("Fade");
            Invoke("TeleportPlayer", delay);
        }
    }

    void TeleportPlayer()
    {
        Application.LoadLevel(nom_scene);
    }
}
