using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : BasePickUp
{
    [Tooltip("Match this ID to the ID of the corresponding weapon")]
    public int m_WeaponID;
    [Tooltip("Number of ammos to increase")]
    public int m_AmmosToAdd;

    protected override void AddToInventory()
    {
        SecondaryWeapon weapon = PlayerInventory.m_Instance.m_Weapons[m_WeaponID] as SecondaryWeapon;

        if (weapon.m_MagazineCapacity - weapon.m_BulletsLeft >= m_AmmosToAdd) weapon.m_BulletsLeft += m_AmmosToAdd;
        else weapon.m_BulletsLeft = weapon.m_MagazineCapacity;

        //Debug.Log(weapon.m_WeaponName + ": " + weapon.m_BulletsLeft);

    }
}
