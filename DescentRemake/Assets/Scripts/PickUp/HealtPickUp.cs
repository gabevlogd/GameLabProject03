/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * author: Gabriele Garofalo
 */
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
            //Debug.Log("Life increased");
            if (m_PickUpSound != null) SoundManager.Instance.PlayWorldSound(m_PickUpSound);
            HUDManager.m_Instance.ShowMessageOnHUD("Life increased");
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Healt is already full");
            HUDManager.m_Instance.ShowMessageOnHUD("Healt is already full");
        }
    }
}
