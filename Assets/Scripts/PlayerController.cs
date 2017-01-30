using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float playerSpeed = 10;
    Vector3 direction;

	// Use this for initialization
	void Start () {
        direction = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        direction = new Vector3(xInput, yInput, 0);
        direction.Normalize();

        transform.Translate(playerSpeed * direction * Time.deltaTime);
	}
}
