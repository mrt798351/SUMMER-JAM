using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public static int bulletCount = 10;
    public static int clipBulletCount = 10; // количество патронов в обойме
    public int health = 100;
    private bool isDead = false;
    private bool groundedPlayer = true;
    private CharacterController controller;

    [SerializeField] private GameObject gameOverPanel;
    private Rigidbody rb;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private LayerMask groundLayer;

    //private Animator _animator;
    //private SpriteRenderer _spriteRenderer;

    public string wearponName = "g22";
    private Vector3 playerVelocity;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
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
        // ПЕРСОНАЖ
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
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
            else  // тут щёлкаем чтобы показать что не патронов
            {
            }
        }
    }
}