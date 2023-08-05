using UnityEngine;

public interface IObserver
{
    void OnNotify(GameObject observerObject, int damage);
}
