using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {
    public string nom_scene;

    void OnTriggerEnter2D(Collider2D coll) {
        Application.LoadLevel(nom_scene);
    }
}
