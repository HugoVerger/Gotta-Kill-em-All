using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public bool isPlayerDead = false;
    public GameObject endHUD;
    bool isGameOver;

    void Start() {
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
        endHUD.SetActive(true);
    }
}
