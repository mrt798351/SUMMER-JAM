using UnityEngine;

public class Weapon : Subject
{
    [SerializeField] private int _damage = 10;
    private string enemyTag = "Enemy";
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            NotifyObservers(other.gameObject, _damage);
        }
        else if (other.CompareTag(playerTag))
        {
            other.GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }
}
