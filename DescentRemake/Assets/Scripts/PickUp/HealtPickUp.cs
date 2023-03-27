using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtPickUp : BasePickUp
{
    public int m_HealtPointToAdd;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player)) AddToInventory();
    }

    protected override void AddToInventory()
    {
        if (PlayerInventory.m_Instance.m_Healt < PlayerInventory.m_Instance.m_MaxHealt)
        {
            if (PlayerInventory.m_Instance.m_MaxHealt - PlayerInventory.m_Instance.m_Healt >= m_HealtPointToAdd) PlayerInventory.m_Instance.m_Healt += m_HealtPointToAdd;
            else PlayerInventory.m_Instance.m_Healt = PlayerInventory.m_Instance.m_MaxHealt;
            Debug.Log("Life increased");
            Destroy(gameObject);
        }
        else Debug.Log("Healt is already full");
    }
}
