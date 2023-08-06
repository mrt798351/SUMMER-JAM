using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public static int bullet = 10;          // патронов в текущей обойме
    public static int clipBulletCount = 10; // количество патронов в обойме (статично)
    public static int bulletCountClip = 100; // количество запасных патронов в обоймах
    private bool isDead = false;
    private bool groundedPlayer = true;
    private CharacterController controller;
    private Rigidbody rb;

    [SerializeField] private GameObject wearpon;
    [SerializeField] private Transform shootPointOfGun;  // точка дула
    [SerializeField] private GameObject gameOverPanel;
    public int health = 100;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private float sensivity = 100;

    [SerializeField] private GameObject[] effectsMuzzlePrefab;

    private GameObject muzzelFlash = null;


    //private Animator _animator;
    //private SpriteRenderer _spriteRenderer;

    public string wearponName = "pm";
    private Vector3 playerVelocity;


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        //_animator = GetComponent<Animator>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();
        if (wearponName == "pm")
        {
            clipBulletCount = 8;
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

        wearpon.transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * sensivity * -1, 0, Input.GetAxis("Mouse X") * Time.deltaTime * sensivity);

        if (Input.GetKeyDown(KeyCode.R))  // выстрел
        {
            Reloading();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))  // выстрел
        {
            Shoot();
        }
    }
    private void FixedUpdate()
    {
        if (muzzelFlash != null)  // удаляем эффукт стрельбы
        {
            Destroy(muzzelFlash);
        }
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
        if (wearponName == "pm")
        {
            if (bullet > 0)
            {
                bullet--;  // минус патрон
                // сам выстрел
                RaycastHit hit;  // луч
                var randEffectMuzzle = Random.Range(0, effectsMuzzlePrefab.Length);
                if (muzzelFlash != null)
                {
                    Destroy(muzzelFlash);
                }
                muzzelFlash = Instantiate(effectsMuzzlePrefab[randEffectMuzzle], shootPointOfGun.position, shootPointOfGun.rotation);

                Ray ray = new Ray(GameObject.Find("Camera").GetComponent<Camera>().transform.position, GameObject.Find("Camera").GetComponent<Camera>().transform.forward);
                if (Physics.Raycast(ray, out hit, 1000, enemy))
                {
                    EnemyController hitObj = hit.transform.gameObject.GetComponent<EnemyController>();
                    hitObj.health -= 20;

                }

            }
            else  // тут щёлкаем чтобы показать что не патронов
            {
            }
        }
    }

    private void Reloading()
    {
        if (wearponName == "pm")
        {
            if (clipBulletCount - bullet <= bulletCountClip) {
                bulletCountClip -= clipBulletCount - bullet;
                bullet = clipBulletCount;
            } else
            {
                bullet += bulletCountClip;
                bulletCountClip = 0;
            }
        }
    }
}