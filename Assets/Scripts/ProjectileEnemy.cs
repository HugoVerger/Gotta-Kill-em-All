using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour {
    public float projectileSpeed = 3;
    public Vector2 direction = new Vector2(0, 1);
    public bool isRocket = false;

    void Update() {
        transform.Translate(direction * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Enemy") {
            if (other.tag == "Player") {
                if (isRocket == false) {
                    other.gameObject.GetComponent<PlayerController>().Damage(1);
                } else {
                    other.gameObject.GetComponent<PlayerController>().Damage(5);
                }
            }
            if (isRocket == false) {
                DestroyObject(gameObject);
            }
        }
    }
}
