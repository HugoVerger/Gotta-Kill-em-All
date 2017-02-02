using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float enemySpeed = 0.6f;
    public int health = 5;
    public Orientation orientation = Orientation.IdleDown;
    public float detectionDistance = 1.5f;
    public float attackPerSecond = 2;
    public float killDistance = 0.2f;
    GameManager gameManager;
    GameObject player;
    Animator animator;
    Vector2 direction;
    float distance;
    float timeSinceLastAttack;
    bool playerFound;
    bool inAttackRange;

    // Use this for initialization
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        animator.speed = 0.45f;
        animator.Play(orientation.ToString());
        direction = new Vector2(0, 0);
        distance = Vector3.Distance(player.transform.position, transform.position);
        timeSinceLastAttack = 0f;
        playerFound = false;
        inAttackRange = false;
    }

    // Update is called once per frame
    void Update() {
        if (gameManager.isPlayerDead == false) {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (playerFound == false) {
                if (distance < detectionDistance) {
                    playerFound = true;
                    transform.Find("Exclamation").gameObject.SetActive(true);
                }
            } else {
                // Player was found !
                inAttackRange = (distance < killDistance);
                orientation = FindOrientation();
                HandleMoving();
                animator.Play(orientation.ToString());
                if (inAttackRange && timeSinceLastAttack > (1 / attackPerSecond)) {
                    timeSinceLastAttack = 0f;
                    Attack();
                }
                timeSinceLastAttack += Time.deltaTime;
            }
        } else {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    Orientation FindOrientation() {
        float distanceX = player.transform.position.x - transform.position.x;
        float distanceY = player.transform.position.y - transform.position.y;
        if (inAttackRange == false) {
            if (Mathf.Abs(distanceX) < killDistance) {
                if (distanceY < 0) {
                    return Orientation.MoveDown;
                } else {
                    return Orientation.MoveUp;
                }
            } else if (Mathf.Abs(distanceY) < killDistance) {
                if (distanceX < 0) {
                    return Orientation.MoveLeft;
                } else {
                    return Orientation.MoveRight;
                }
            } else {
                if (Mathf.Abs(distanceX) < Mathf.Abs(distanceY)) {
                    if (distanceX < 0) {
                        return Orientation.MoveLeft;
                    } else {
                        return Orientation.MoveRight;
                    }
                } else {
                    if (distanceY < 0) {
                        return Orientation.MoveDown;
                    } else {
                        return Orientation.MoveUp;
                    }
                }
            }
        } else {
            if (Mathf.Abs(distanceX) > Mathf.Abs(distanceY)) {
                if (distanceX < 0) {
                    return Orientation.AttackLeft;
                } else {
                    return Orientation.AttackRight;
                }
            } else {
                if (distanceY < 0) {
                    return Orientation.AttackDown;
                } else {
                    return Orientation.AttackUp;
                }
            }
        }
    }

    void HandleMoving() {
        direction = new Vector2(0, 0);
        if (orientation == Orientation.MoveLeft) {
            direction.x--;
        } else if (orientation == Orientation.MoveRight) {
            direction.x++;
        } else if (orientation == Orientation.MoveUp) {
            direction.y++;
        } else if (orientation == Orientation.MoveDown) {
            direction.y--;
        }
        GetComponent<Rigidbody2D>().velocity = direction * enemySpeed;
    }

    void Attack() {
        player.GetComponent<PlayerController>().Damage(1);
    }

    public void Damage(int damageDone) {
        health -= damageDone;
        if (health <= 0) {
            DestroyObject(gameObject);
        }
    }
}
