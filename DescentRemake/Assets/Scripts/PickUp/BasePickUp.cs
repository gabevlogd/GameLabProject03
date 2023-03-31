using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePickUp : MonoBehaviour
{

    public AudioClip m_PickUpSound;

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
