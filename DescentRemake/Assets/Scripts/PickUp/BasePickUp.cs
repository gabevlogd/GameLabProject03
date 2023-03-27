using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePickUp : MonoBehaviour
{

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            AddToInventory();
            Destroy(gameObject);
        }
    }

    protected abstract void AddToInventory();
}
