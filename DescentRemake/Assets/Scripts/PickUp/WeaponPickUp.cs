using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : BasePickUp
{
    [Tooltip("Match this ID to the ID of the corresponding weapon")]
    public int m_WeaponID;


    protected override void AddToInventory()
    {
        PlayerInventory.m_Instance.m_UnlockedWeapon[m_WeaponID] = true;
    }

}
