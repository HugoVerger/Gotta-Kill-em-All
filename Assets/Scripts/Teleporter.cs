using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    public Orientation orientation = Orientation.MoveUp;
    public float delay = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<PlayerController>().orientation == orientation)
        {
            GameObject.Find("BlackScreen").GetComponent<Animator>().Play("Fade");
            Invoke("TeleportPlayer", delay);
        }
    }

    void TeleportPlayer()
    {
        GameObject.Find("Player").transform.position = destination.position;
    }
}
