/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : BasePickUp
{
    [Tooltip("Match this ID to the ID of the corresponding weapon")]
    public int m_WeaponID;


    protected override void AddToInventory()
    {
        PlayerInventory.m_Instance.m_Weapons[m_WeaponID].m_Unlocked = true;
        if (m_PickUpSound != null) SoundManager.Instance.PlayWorldSound(m_PickUpSound);
        HUDManager.m_Instance.ShowMessageOnHUD(PlayerInventory.m_Instance.m_Weapons[m_WeaponID].m_WeaponName + " ADDED TO WEAPONS");
    }

}
