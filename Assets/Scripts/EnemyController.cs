using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 0.7f;
    public int health = 3;
    public bool isRange = false;
    public float detectionDistance = 1.5f;
    public float killDistance = 0.3f;
    public float bulletsPerSecond = 8;
    public float schlassPerSecond = 2;
    public Projectile projectile;
    public Orientation orientation = Orientation.IdleDown;
    public float strafeRange;
    Vector2 originalPosition;
    GameManager gameManager;
    GameObject player;
    Vector2 direction;
    bool playerFound;
    bool inAttackRange;
    float timeSinceLastAttack = 0f;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        direction = new Vector2(0, 0);
        playerFound = false;
        inAttackRange = false;
        originalPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPlayerDead == false)
        {
            HandleMoving();
            if (isRange)
            {
                if (timeSinceLastAttack > (1 / bulletsPerSecond))
                {
                    timeSinceLastAttack = 0f;
                    Fire();
                }
            }
            else
            {
                if (inAttackRange && timeSinceLastAttack > (1 / schlassPerSecond))
                {
                    timeSinceLastAttack = 0f;
                    Attack();
                }
            }
            timeSinceLastAttack += Time.deltaTime;
        }
    }

    Orientation FindNextMove()
    {
        if (isRange == false)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            inAttackRange = (distance < killDistance);
            if (inAttackRange == false && (distance < detectionDistance || playerFound))
            {
                playerFound = true;
                float distanceX = player.transform.position.x - transform.position.x;
                float distanceY = player.transform.position.y - transform.position.y;
                if (Mathf.Abs(distanceX) < 0.2f)
                {
                    if (distanceY < 0)
                    {
                        return Orientation.MoveDown;
                    }
                    else
                    {
                        return Orientation.MoveUp;
                    }
                }
                else if (Mathf.Abs(distanceY) < 0.2f)
                {
                    if (distanceX < 0)
                    {
                        return Orientation.MoveLeft;
                    }
                    else
                    {
                        return Orientation.MoveRight;
                    }
                }
                else
                {
                    if (Mathf.Abs(distanceX) < Mathf.Abs(distanceY))
                    {
                        if (distanceX < 0)
                        {
                            return Orientation.MoveLeft;
                        }
                        else
                        {
                            return Orientation.MoveRight;
                        }
                    }
                    else
                    {
                        if (distanceY < 0)
                        {
                            return Orientation.MoveDown;
                        }
                        else
                        {
                            return Orientation.MoveUp;
                        }
                    }
                }
            }
        }
        return orientation;
    }

    void HandleMoving()
    {
        if (isRange == false)
        {
            Orientation nextMove = FindNextMove();
            direction = new Vector2(0, 0);
            if (nextMove == Orientation.MoveLeft)
            {
                direction.x--;
            }
            else if (nextMove == Orientation.MoveRight)
            {
                direction.x++;
            }
            else if (nextMove == Orientation.MoveUp)
            {
                direction.y++;
            }
            else if (nextMove == Orientation.MoveDown)
            {
                direction.y--;
            }
            if (direction.magnitude != 0)
            {
                orientation = nextMove;
            }
            else
            {
                if (orientation == Orientation.MoveLeft)
                {
                    orientation = Orientation.IdleLeft;
                }
                else if (orientation == Orientation.MoveRight)
                {
                    orientation = Orientation.IdleRight;
                }
                else if (orientation == Orientation.MoveUp)
                {
                    orientation = Orientation.IdleUp;
                }
                else if (orientation == Orientation.MoveDown)
                {
                    orientation = Orientation.IdleDown;
                }
            }
        }
        else
        {
            if (direction.x == 0 && direction.y == 0)
            {
                if (orientation == Orientation.MoveLeft || orientation == Orientation.MoveRight)
                {
                    direction = new Vector2(0, 1);
                }
                else if (orientation == Orientation.MoveUp || orientation == Orientation.MoveDown)
                {
                    direction = new Vector2(1, 0);
                }
            }
            else if (orientation == Orientation.MoveLeft || orientation == Orientation.MoveRight)
            {
                if (transform.position.y < originalPosition.y - strafeRange)
                {
                    direction = new Vector2(0, 1);
                }
                else if (transform.position.y > originalPosition.y + strafeRange)
                {
                    direction = new Vector2(0, -1);
                }
            }
            else if (orientation == Orientation.MoveUp || orientation == Orientation.MoveDown)
            {
                if (transform.position.x < originalPosition.x - strafeRange)
                {
                    direction = new Vector2(1, 0);
                }
                else if (transform.position.x > originalPosition.x + strafeRange)
                {
                    direction = new Vector2(-1, 0);
                }
            }
        }
        GetComponent<Rigidbody2D>().velocity = direction * enemySpeed;
    }

    void Fire()
    {
        if (orientation == Orientation.MoveLeft || orientation == Orientation.IdleLeft)
        {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 90));
            temp.direction = new Vector2(0, 1);
        }
        else if (orientation == Orientation.MoveRight || orientation == Orientation.IdleRight)
        {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 90));
            temp.direction = new Vector2(0, -1);
        }
        else if (orientation == Orientation.MoveUp || orientation == Orientation.IdleUp)
        {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.direction = new Vector2(0, 1);
        }
        else if (orientation == Orientation.MoveDown || orientation == Orientation.IdleDown)
        {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.direction = new Vector2(0, -1);
        }
    }

    void Attack()
    {
        player.GetComponent<PlayerController>().Damage(1);
    }
}
