using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChenTrigger : MonoBehaviour {
    public GameObject ak47Trigger;
    public GameObject dialogueTriggerChen;
    GameObject chen;
    GameObject player;
    Image textBubble;
    Text textDialogue;
    Vector3 chenDirection;
    Vector3 sachaDirection;
    Animator sachaAnimator;
    Animator chenAnimator;
    bool hasPlayerReached;
    bool readyToMove;

    // Use this for initialization
    void Start() {
        chen = GameObject.Find("Chen");
        player = GameObject.Find("Player");
        textBubble = GameObject.Find("TextBubble").GetComponent<Image>();
        textDialogue = GameObject.Find("TextDialogue").GetComponent<Text>();
        chenDirection = new Vector3(0, 0, 0);
        sachaDirection = new Vector3(0, 0, 0);
        chenAnimator = chen.GetComponent<Animator>();
        chenAnimator.speed = 0.45f;
        sachaAnimator = player.GetComponent<Animator>();
        hasPlayerReached = false;
        readyToMove = false;
    }

    // Update is called once per frame
    void Update() {
        if (hasPlayerReached) {
            if (readyToMove) {
                player.transform.Translate(0.35f * sachaDirection * Time.deltaTime);
                chen.transform.Translate(0.35f * chenDirection * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player" && hasPlayerReached == false) {
            hasPlayerReached = true;
            player.transform.position = new Vector3(-0.09f, -3.1f, 0);
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.isFrozen = true;
            playerController.orientation = Orientation.MoveDown;
            player.transform.Find("Exclamation").gameObject.SetActive(true);
            textBubble.enabled = true;
            textDialogue.text = "CHEN: Hey ! Attends ! Ne t'en vas pas !";
            Invoke("StartMovingChen", 2f);
        }
    }

    void StartMovingChen() {
        textBubble.enabled = false;
        textDialogue.text = "";
        readyToMove = true;
        chen.transform.position = new Vector3(chen.transform.position.x, -4, 0);
        chenDirection = new Vector3(0, 1, 0);
        chenAnimator.Play("MoveUp");
        Invoke("StopChenBeforeSacha", 2.2f);
    }

    void StopChenBeforeSacha() {
        readyToMove = false;
        textBubble.enabled = true;
        textDialogue.text = "CHEN: C'est dangereux de partir seul, suis moi!";
        chenAnimator.Play("IdleUp");
        Invoke("StartMovingThemBoth", 2f);
    }

    void StartMovingThemBoth() {
        readyToMove = true;
        textBubble.enabled = false;
        textDialogue.text = "";
        chenDirection = new Vector3(0, -1, 0);
        sachaDirection = new Vector3(0, -1, 0);
        sachaAnimator.Play("MoveDown");
        chenAnimator.Play("MoveDown");
        Invoke("MoveThemBothToTheLeft", 1.4f);
    }

    void MoveThemBothToTheLeft() {
        chenDirection = new Vector3(-1, 0, 0);
        sachaDirection = new Vector3(-1, 0, 0);
        sachaAnimator.Play("MoveLeft");
        chenAnimator.Play("MoveLeft");
        Invoke("MoveThemBothDown", 0.9f);
    }

    void MoveThemBothDown() {
        chenDirection = new Vector3(0, -1, 0);
        sachaDirection = new Vector3(0, -1, 0);
        sachaAnimator.Play("MoveDown");
        chenAnimator.Play("MoveDown");
        Invoke("MoveChenRight", 3.25f);
        Invoke("MoveSachaRight", 3.625f);
    }

    void MoveChenRight() {
        chenDirection = new Vector3(1, 0, 0);
        chenAnimator.Play("MoveRight");
        Invoke("MakeChenEnter", 1.8f);
    }

    void MoveSachaRight() {
        sachaDirection = new Vector3(1, 0, 0);
        sachaAnimator.Play("MoveRight");
        Invoke("MakeSachaEnter", 1.82f);
    }

    void MakeChenEnter() {
        chenDirection = new Vector3(0, 1, 0);
        chenAnimator.Play("MoveUp");
        Invoke("TeleportChen", 0.2f);
    }

    void TeleportChen() {
        chenAnimator.Play("IdleDown");
        chen.transform.position = new Vector3(5, -14.05f, 0);
        chen.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    void MakeSachaEnter() {
        readyToMove = false;
        sachaDirection = new Vector3(0, 0, 0);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.isFrozen = false;
        playerController.orientation = Orientation.IdleUp;
        sachaAnimator.Play("IdleUp");
        ak47Trigger.SetActive(true);
        dialogueTriggerChen.SetActive(true);
    }
}
