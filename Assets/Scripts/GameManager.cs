using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool isPlayerDead = false;
    public GameObject endHUD;
    public AudioClip endSound;
    public AudioSource audioSource;
    bool isGameOver;

    void Start() {
        if (endHUD == null) {
            endHUD = GameObject.Find("EndHud");
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = endSound;
        audioSource.volume = 0.5f;
        isGameOver = false;
    }

    void Update() {
        if (isGameOver == false) {
            if (isPlayerDead == true) {
                isGameOver = true;
                displayEndHUD();
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }

    void displayEndHUD() {
        audioSource.clip = endSound;
        audioSource.volume = 0.5f;
        audioSource.Play();
        endHUD.SetActive(true);
    }
}
