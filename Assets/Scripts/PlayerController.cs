using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation { IdleLeft, IdleRight, IdleUp, IdleDown, MoveLeft, MoveRight, MoveUp, MoveDown };

public class PlayerController : MonoBehaviour {
    public float playerSpeed = 1;
    public float bulletsPerSecond = 8;
    public Projectile projectile;
    public Orientation orientation = Orientation.IdleDown;
    public TextMesh textMesh;
    public Animation moveLeft;
    public Animation moveRight;
    public Animation moveUp;
    public Animation moveDown;
    public Animation idleLeft;
    public Animation idleRight;
    public Animation idleUp;
    public Animation idleDown;
    Vector2 oldDirection;
    Vector2 newDirection;
    bool fire;
    float timeSinceLastShot = 0f;

    // Use this for initialization
    void Start() {
        fire = false;
        oldDirection = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update() {
        HandleInput();
        if (fire) {
            if (timeSinceLastShot > (1 / bulletsPerSecond)) {
                timeSinceLastShot = 0f;
                Fire();
            } else {
                timeSinceLastShot += Time.deltaTime;
            }
        }
    }

    void HandleInput() {
        newDirection = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow)) {
            newDirection.x--;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            newDirection.x++;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            newDirection.y++;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            newDirection.y--;
        }
        if (newDirection.x != 0 && newDirection.y != 0) {
            if (newDirection.x == oldDirection.x) {
                newDirection.x = 0;
            } else {
                newDirection.y = 0;
            }
        } else {
            oldDirection = newDirection;
        }
        GetComponent<Rigidbody2D>().velocity = newDirection * playerSpeed;

        HandleOrientation();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            fire = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) {
            fire = false;
            timeSinceLastShot = 0f;
        }
    }

    void HandleOrientation() {
        if (fire) {
            if (newDirection.magnitude == 0) {
                if (orientation == Orientation.MoveRight) {
                    orientation = Orientation.IdleRight;
                } else if (orientation == Orientation.MoveLeft) {
                    orientation = Orientation.IdleLeft;
                } else if (orientation == Orientation.MoveUp) {
                    orientation = Orientation.IdleUp;
                } else if (orientation == Orientation.MoveDown) {
                    orientation = Orientation.IdleDown;
                }
            } else {
                if (orientation == Orientation.IdleRight) {
                    orientation = Orientation.MoveRight;
                } else if (orientation == Orientation.IdleLeft) {
                    orientation = Orientation.MoveLeft;
                } else if (orientation == Orientation.IdleUp) {
                    orientation = Orientation.MoveUp;
                } else if (orientation == Orientation.IdleDown) {
                    orientation = Orientation.MoveDown;
                }
            }
        } else {
            if (newDirection.x == 1) {
                orientation = Orientation.MoveRight;
            } else if (newDirection.x == -1) {
                orientation = Orientation.MoveLeft;
            } else if (newDirection.y == 1) {
                orientation = Orientation.MoveUp;
            } else if (newDirection.y == -1) {
                orientation = Orientation.MoveDown;
            } else {
                if (orientation == Orientation.MoveLeft) {
                    orientation = Orientation.IdleLeft;
                } else if (orientation == Orientation.MoveRight) {
                    orientation = Orientation.IdleRight;
                } else if (orientation == Orientation.MoveUp) {
                    orientation = Orientation.IdleUp;
                } else if (orientation == Orientation.MoveDown) {
                    orientation = Orientation.IdleDown;
                }
            }
        }
        textMesh.text = orientation.ToString();
    }

    void Fire() {
        if (orientation == Orientation.MoveLeft || orientation == Orientation.IdleLeft) {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 90));
            temp.direction = new Vector2(0, 1);
        } else if (orientation == Orientation.MoveRight || orientation == Orientation.IdleRight) {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 90));
            temp.direction = new Vector2(0, -1);
        } else if (orientation == Orientation.MoveUp || orientation == Orientation.IdleUp) {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.direction = new Vector2(0, 1);
        } else if (orientation == Orientation.MoveDown || orientation == Orientation.IdleDown) {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.direction = new Vector2(0, -1);
        }
    }
}
