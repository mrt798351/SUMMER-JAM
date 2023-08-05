using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Subject
{
    private int _bulletCount = 10;
    private int _health = 100;
    private bool _isDead = false;

    [SerializeField] private GameObject _gameOverPanel;

    public int BulletCount
    {
        get { return _bulletCount; }
        set
        {
            _bulletCount = value;
            if (BulletText != null)
            {
                BulletText.text = "Пули: " + _bulletCount.ToString();
            }
        }
    }

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (HealthBar != null)
            {
                HealthBar.value = _health;
            }
        }
    }

    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }

    private Rigidbody _rb;
    //private Animator _animator;
    //private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Slider HealthBar;
    [SerializeField] private TextMeshProUGUI BulletText;

    private bool _isGrounded = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_animator = GetComponent<Animator>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();

        // Find HealthBar and BulletText if not assigned in the inspector
        if (HealthBar == null)
        {
            HealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        }
        if (BulletText == null)
        {
            BulletText = GameObject.Find("BulletText").GetComponent<TextMeshProUGUI>();
        }

        // Set initial values for HealthBar and BulletText
        Health = Health;
        BulletCount = BulletCount;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0)
        {
            //_spriteRenderer.flipX = false;
            _rb.velocity = new Vector3(_moveSpeed, _rb.velocity.y);
            //_animator.SetBool("isRunning", true);
        }
        else if (horizontalInput < 0)
        {
            //_spriteRenderer.flipX = true;
            _rb.velocity = new Vector3(-_moveSpeed, _rb.velocity.y);
            //_animator.SetBool("isRunning", true);
        }
        else
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y);
            //_animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(new Vector3(0, _jumpForce), ForceMode.Impulse);
            //_animator.SetTrigger("jump");
            _isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && BulletCount > 0)
        {
            Shoot();
            BulletCount--;
        }
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, _groundLayer);
        //_animator.SetBool("isGrounded", _isGrounded);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0 && !IsDead)
        {
            IsDead = true;
            StartCoroutine(GameOver());
        }
        else
        {
            NotifyObservers(gameObject, damage);
        }
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > 100)
        {
            Health = 100;
        }
    }

    private void Shoot()
    {
        // Code for shooting
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        _gameOverPanel.SetActive(true);
        Destroy(gameObject);
    }
}