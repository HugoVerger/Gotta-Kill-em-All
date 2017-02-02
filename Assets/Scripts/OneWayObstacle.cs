using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayObstacle : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.transform.position.y > transform.position.y)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
