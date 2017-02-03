using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47PickUp : MonoBehaviour {
    public RuntimeAnimatorController newController;
    public GameObject oldTP;
    public GameObject EndOfTutorialTP;
    bool isPlayerInside;

    // Use this for initialization
    void Start() {
        isPlayerInside = false;
    }

    // Update is called once per frame
    void Update() {
        if (isPlayerInside) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                GameObject.Find("Player").GetComponent<Animator>().runtimeAnimatorController = newController;
                GameObject.Find("Player").GetComponent<Animator>().Play("IdleUp");
                GameObject.Find("Player").GetComponent<PlayerController>().canShoot = true;
                GameObject.Find("MusicPlayer").GetComponent<AudioSource>().mute = true;
                oldTP.SetActive(false);
                EndOfTutorialTP.SetActive(true);
                DestroyObject(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            isPlayerInside = false;
        }
    }
}
