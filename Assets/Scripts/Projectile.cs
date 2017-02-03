using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float projectileSpeed = 3;
    public Vector2 direction = new Vector2(0, 1);

    void Update() {
        transform.Translate(direction * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Player") {
            if (other.tag == "Enemy") {
                if (other.gameObject.GetComponent<EnemyController>() != null) {
                    other.gameObject.GetComponent<EnemyController>().Damage(1);
                } else {
                    other.gameObject.GetComponent<EnemyRangedController>().Damage(1);
                }
            } else if (other.tag == "Chen") {
                DestroyObject(other.gameObject);
            }
            DestroyObject(gameObject);
        }
    }
}
