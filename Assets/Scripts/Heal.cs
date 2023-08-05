using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private int healAmount = 10;
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.GetComponent<PlayerController>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}