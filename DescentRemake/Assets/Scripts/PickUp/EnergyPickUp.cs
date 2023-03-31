using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickUp : BasePickUp
{
    public int m_EnergyToAdd;

    protected override void AddToInventory()
    {
        if (PlayerInventory.m_Instance.m_Energy < PlayerInventory.m_Instance.m_MaxEnergy)
        {
            if (PlayerInventory.m_Instance.m_MaxEnergy - PlayerInventory.m_Instance.m_Energy >= m_EnergyToAdd) PlayerInventory.m_Instance.m_Energy += m_EnergyToAdd;
            else PlayerInventory.m_Instance.m_Energy = PlayerInventory.m_Instance.m_MaxEnergy;
            //Debug.Log("Energy increased");
            if (m_PickUpSound != null) SoundManager.Instance.PlayWorldSound(m_PickUpSound);
            HUDManager.m_Instance.ShowMessageOnHUD("Energy increased");
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Energy is already full");
            HUDManager.m_Instance.ShowMessageOnHUD("Energy is already full");
        }
    }
}
