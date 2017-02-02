using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation { IdleLeft, IdleRight, IdleUp, IdleDown, MoveLeft, MoveRight, MoveUp, MoveDown, AttackLeft, AttackRight, AttackUp, AttackDown };

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 0.7f;
    public int health = 5;
    public Orientation orientation = Orientation.IdleDown;
    public float bulletsPerSecond = 10;
    public Projectile projectile;
    public bool isFrozen = false;
    Animator animator;
    Vector2 oldDirection;
    Vector2 newDirection;
    float timeSinceLastShot;
    bool fire;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0.45f;
        animator.Play(orientation.ToString());
        oldDirection = new Vector2(0, 0);
        timeSinceLastShot = 0f;
        fire = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        if (fire && timeSinceLastShot > (1 / bulletsPerSecond))
        {
            timeSinceLastShot = 0f;
            Fire();
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    void HandleInput()
    {
        newDirection = new Vector2(0, 0);
        if (isFrozen == false)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                newDirection.x--;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                newDirection.x++;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                newDirection.y++;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                newDirection.y--;
            }
        }
        if (newDirection.x != 0 && newDirection.y != 0)
        {
            if (newDirection.x == oldDirection.x)
            {
                newDirection.x = 0;
            }
            else
            {
                newDirection.y = 0;
            }
        }
        else
        {
            oldDirection = newDirection;
        }
        GetComponent<Rigidbody2D>().velocity = newDirection * playerSpeed;

        HandleOrientation();

        if (isFrozen == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                fire = true;
            }
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            {
                fire = false;
            }
        }
        else
        {
            fire = false;
        }
    }

    void HandleOrientation()
    {
        if (fire)
        {
            if (newDirection.magnitude == 0)
            {
                if (orientation == Orientation.MoveRight)
                {
                    orientation = Orientation.IdleRight;
                    animator.Play("IdleRight");
                }
                else if (orientation == Orientation.MoveLeft)
                {
                    orientation = Orientation.IdleLeft;
                    animator.Play("IdleLeft");
                }
                else if (orientation == Orientation.MoveUp)
                {
                    orientation = Orientation.IdleUp;
                    animator.Play("IdleUp");
                }
                else if (orientation == Orientation.MoveDown)
                {
                    orientation = Orientation.IdleDown;
                    animator.Play("IdleDown");
                }
            }
            else
            {
                if (orientation == Orientation.IdleRight)
                {
                    orientation = Orientation.MoveRight;
                    animator.Play("MoveRight");
                }
                else if (orientation == Orientation.IdleLeft)
                {
                    orientation = Orientation.MoveLeft;
                    animator.Play("MoveLeft");
                }
                else if (orientation == Orientation.IdleUp)
                {
                    orientation = Orientation.MoveUp;
                    animator.Play("MoveUp");
                }
                else if (orientation == Orientation.IdleDown)
                {
                    orientation = Orientation.MoveDown;
                    animator.Play("MoveDown");
                }
            }
        }
        else
        {
            if (newDirection.x == 1)
            {
                orientation = Orientation.MoveRight;
                animator.Play("MoveRight");
            }
            else if (newDirection.x == -1)
            {
                orientation = Orientation.MoveLeft;
                animator.Play("MoveLeft");
            }
            else if (newDirection.y == 1)
            {
                orientation = Orientation.MoveUp;
                animator.Play("MoveUp");
            }
            else if (newDirection.y == -1)
            {
                orientation = Orientation.MoveDown;
                animator.Play("MoveDown");
            }
            else
            {
                if (orientation == Orientation.MoveLeft)
                {
                    orientation = Orientation.IdleLeft;
                    animator.Play("IdleLeft");
                }
                else if (orientation == Orientation.MoveRight)
                {
                    orientation = Orientation.IdleRight;
                    animator.Play("IdleRight");
                }
                else if (orientation == Orientation.MoveUp)
                {
                    orientation = Orientation.IdleUp;
                    animator.Play("IdleUp");
                }
                else if (orientation == Orientation.MoveDown)
                {
                    orientation = Orientation.IdleDown;
                    animator.Play("IdleDown");
                }
            }
        }
    }

    void Fire()
    {
        if (orientation == Orientation.MoveLeft || orientation == Orientation.IdleLeft)
        {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 270));
            temp.transform.Translate(new Vector3(0.025f, -0.120f, 0));
            temp.direction = new Vector2(0, -1);
        }
        else if (orientation == Orientation.MoveRight || orientation == Orientation.IdleRight)
        {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 90));
            temp.transform.Translate(new Vector3(-0.025f, -0.120f, 0));
            temp.direction = new Vector2(0, -1);
        }
        else if (orientation == Orientation.MoveUp || orientation == Orientation.IdleUp)
        {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.transform.Rotate(new Vector3(0, 0, 180));
            temp.transform.Translate(new Vector3(0.025f, -0.120f, 0));
            temp.direction = new Vector2(0, -1);
        }
        else if (orientation == Orientation.MoveDown || orientation == Orientation.IdleDown)
        {
            Projectile temp = Instantiate<Projectile>(projectile, transform.position, transform.rotation);
            temp.transform.Translate(new Vector3(0.025f, -0.120f, 0));
            temp.direction = new Vector2(0, -1);
        }
        GetComponent<AudioSource>().Play();
    }

    public void Damage(int damageDone)
    {
        health -= damageDone;
        if (health <= 0)
        {
            health = 0;
            HealthBar test = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            test.UpdateHealthBar(health);
            GameObject.Find("GameManager").GetComponent<GameManager>().isPlayerDead = true;
            transform.DetachChildren();
            DestroyObject(gameObject);
        }
        else
        {
            HealthBar test = GameObject.Find("HealthBar").GetComponent<HealthBar>();
            test.UpdateHealthBar(health);
        }
    }
}
