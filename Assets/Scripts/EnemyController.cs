using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 0.7f;
    public bool isSchlassing = true;
    public float detectionDistance = 1.5f;
    public float killDistance = 0.18f;
    public Orientation orientation = Orientation.IdleDown;
    public TextMesh textMesh;
    public float bulletsPerSecond = 8;
    public Projectile projectile;
    public Animation moveLeft;
    public Animation moveRight;
    public Animation moveUp;
    public Animation moveDown;
    public Animation idleLeft;
    public Animation idleRight;
    public Animation idleUp;
    public Animation idleDown;
    GameObject player;
    Vector2 direction;
    bool fire;
    float timeSinceLastShot = 0f;

    // Use this for initialization
    void Start()
    {
        direction = new Vector2(0, 0);
        fire = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FindNextMove();
        HandleMoving();
        if (fire)
        {
            if (timeSinceLastShot > (1 / bulletsPerSecond))
            {
                timeSinceLastShot = 0f;
                Fire();
            }
            else
            {
                timeSinceLastShot += Time.deltaTime;
            }
        }
    }

    Orientation FindNextMove()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < killDistance)
        {
            DestroyObject(gameObject);
        }
        else
        {
            if (distance < detectionDistance)
            {
                float distanceX = player.transform.position.x - transform.position.x;
                float distanceY = player.transform.position.y - transform.position.y;
                if (Mathf.Abs(distanceX) < killDistance)
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
                else if (Mathf.Abs(distanceY) < killDistance)
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
        return Orientation.IdleDown;
    }

    void HandleMoving()
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
        GetComponent<Rigidbody2D>().velocity = direction * enemySpeed;
        textMesh.text = orientation.ToString();

        /*if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            fire = true;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            fire = false;
            timeSinceLastShot = 0f;
        }*/
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
}
