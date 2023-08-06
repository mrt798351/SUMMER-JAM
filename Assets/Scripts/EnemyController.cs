using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int AllBullet = 50;
    [SerializeField] public int health = 100;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.0f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }
}
