using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController : MonoBehaviour {
    public float enemySpeed = 0.6f;
    public int health = 3;
    public Orientation orientation = Orientation.IdleDown;
    public float detectionDistance = 1.5f;
    public float bulletsPerSecond = 10;
    public float strafeRange = 0.2f;
    public ProjectileEnemy projectileEnemy;
    GameManager gameManager;
    GameObject player;
    Animator animator;
    Vector2 originalPosition;
    Vector2 direction;
    float timeSinceLastShot;
    bool playerFound;

    // Use this for initialization
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        animator.speed = 0.45f;
        animator.Play(orientation.ToString());
        originalPosition = new Vector2(transform.position.x, transform.position.y);
        direction = new Vector2(0, 0);
        timeSinceLastShot = 0f;
        playerFound = false;
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.isPlayerDead == false) {
            if (playerFound == false) {
                if (Vector3.Distance(transform.position, player.transform.position) < detectionDistance) {
                    playerFound = true;
                    HandleOrientation();
                    transform.Find("Exclamation").gameObject.SetActive(true);
                }
            } else {
                // Player was found !
                HandleMoving();
                if (timeSinceLastShot > (1 / bulletsPerSecond)) {
                    timeSinceLastShot = 0f;
                    Fire();
                }
                timeSinceLastShot += Time.deltaTime;
            }
        } else {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void HandleOrientation() {
        if (orientation == Orientation.IdleRight) {
            orientation = Orientation.MoveRight;
        } else if (orientation == Orientation.IdleLeft) {
            orientation = Orientation.MoveLeft;
        } else if (orientation == Orientation.IdleUp) {
            orientation = Orientation.MoveUp;
        } else if (orientation == Orientation.IdleDown) {
            orientation = Orientation.MoveDown;
        }
        animator.Play(orientation.ToString());
    }

    void HandleMoving() {
        if (direction.x == 0 && direction.y == 0) {
            if (orientation == Orientation.MoveLeft || orientation == Orientation.MoveRight) {
                direction = new Vector2(0, 1);
            } else if (orientation == Orientation.MoveUp || orientation == Orientation.MoveDown) {
                direction = new Vector2(1, 0);
            }
        } else if (orientation == Orientation.MoveLeft || orientation == Orientation.MoveRight) {
            if (transform.position.y < originalPosition.y - strafeRange) {
                direction = new Vector2(0, 1);
            } else if (transform.position.y > originalPosition.y + strafeRange) {
                direction = new Vector2(0, -1);
            }
        } else if (orientation == Orientation.MoveUp || orientation == Orientation.MoveDown) {
            if (transform.position.x < originalPosition.x - strafeRange) {
                direction = new Vector2(1, 0);
            } else if (transform.position.x > originalPosition.x + strafeRange) {
                direction = new Vector2(-1, 0);
            }
        }
        GetComponent<Rigidbody2D>().velocity = direction * enemySpeed;
    }

    void Fire() {
        if (orientation == Orientation.MoveLeft || orientation == Orientation.IdleLeft) {
            ProjectileEnemy temp = Instantiate<ProjectileEnemy>(projectileEnemy, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 270));
            temp.transform.Translate(new Vector3(0.025f, -0.120f, 0));
            temp.direction = new Vector2(0, -1);
        } else if (orientation == Orientation.MoveRight || orientation == Orientation.IdleRight) {
            ProjectileEnemy temp = Instantiate<ProjectileEnemy>(projectileEnemy, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 90));
            temp.transform.Translate(new Vector3(-0.025f, -0.120f, 0));
            temp.direction = new Vector2(0, -1);
        } else if (orientation == Orientation.MoveUp || orientation == Orientation.IdleUp) {
            ProjectileEnemy temp = Instantiate<ProjectileEnemy>(projectileEnemy, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 180));
            temp.transform.Translate(new Vector3(0.025f, -0.120f, 0));
            temp.direction = new Vector2(0, -1);
        } else if (orientation == Orientation.MoveDown || orientation == Orientation.IdleDown) {
            ProjectileEnemy temp = Instantiate<ProjectileEnemy>(projectileEnemy, transform.position, transform.rotation);
            temp.transform.Translate(new Vector3(0.025f, -0.120f, 0));
            temp.direction = new Vector2(0, -1);
        }
        GetComponent<AudioSource>().Play();
    }

    public void Damage(int damageDone) {
        health -= damageDone;
        if (health <= 0) {
            DestroyObject(gameObject);
        }
    }
}
