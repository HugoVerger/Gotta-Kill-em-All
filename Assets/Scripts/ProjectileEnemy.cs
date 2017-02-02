using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public float projectileSpeed = 3;
    public Vector2 direction = new Vector2(0, 1);

    void Update()
    {
        transform.Translate(direction * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy")
        {
            DestroyObject(gameObject);
            if (other.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerController>().Damage(1);
            }
        }
    }
}
