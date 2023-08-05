using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static int bulletCount = 10;
    public static int clipBulletCount = 10; // патронов в обойме
    public int health = 100;
    public bool isDead = false;

    [SerializeField] private GameObject gameOverPanel;

    private Rigidbody rb;
    //private Animator _animator;
    //private SpriteRenderer _spriteRenderer;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;

    private bool isGrounded = true;

    public string wearponName = "g22";

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        //_animator = GetComponent<Animator>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        if (wearponName == "g22")
        {
            clipBulletCount = 10;
        }
    }

    private void Update()
    {
        /*
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
        {
            //_spriteRenderer.flipX = false;
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y);
            //_animator.SetBool("isRunning", true);
        }
        else if (horizontalInput < 0)
        {
            //_spriteRenderer.flipX = true;
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y);
            //_animator.SetBool("isRunning", true);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
            //_animator.SetBool("isRunning", false);
        }*/

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce), ForceMode.Impulse);
            //_animator.SetTrigger("jump");
            //isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        //isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
        //_animator.SetBool("isGrounded", _isGrounded);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            gameOverPanel.SetActive(true);
        }
    }

    private void Shoot()
    {
        if (wearponName == "g22")
        {
            if (bulletCount > 0)
            {
                bulletCount--;
            }
            else  // тут надо издать звук щёлканья чтоб уведомить игрока о пустой обойме
            {

            }
        }

    }
}