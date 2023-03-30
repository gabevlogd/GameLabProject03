using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDropPickUp : BasePickUp
{
    private int[] m_Ammo;
    private PlayerInventory m_player;

    private void Awake()
    {
        m_Ammo = new int[PlayerInventory.m_Instance.m_Weapons.Count];
        m_player = PlayerInventory.m_Instance;

        EmptyWeaponAmmo();
    }

    protected override void AddToInventory()
    {
        for (int i = 0; i < m_player.m_Weapons.Count; i++)
        {
            int weaponID = m_player.m_WeaponsPrefabs[i].m_WeaponID; //key of the dictionary m_Weapons

            if (m_player.m_Weapons[weaponID].m_WeaponType != WeaponType.Secondary) continue; // if weapon is primary skip cause primary weapon has no ammo 

            SecondaryWeapon weapon = m_player.m_Weapons[weaponID] as SecondaryWeapon;

            if (weapon.m_MagazineCapacity - m_player.m_Weapons[weaponID].m_BulletsLeft >= m_Ammo[i]) m_player.m_Weapons[weaponID].m_BulletsLeft += m_Ammo[i];
            else m_player.m_Weapons[weaponID].m_BulletsLeft = weapon.m_MagazineCapacity;
        }
        HUDManager.m_Instance.ShowMessageOnHUD("AMMUNITION RECOVERED");
    }

    private void EmptyWeaponAmmo()
    {
        for (int i = 0; i < m_player.m_Weapons.Count; i++)
        {
            int weaponID = m_player.m_WeaponsPrefabs[i].m_WeaponID; //key of the dictionary m_Weapons

            if (m_player.m_Weapons[weaponID].m_BulletsLeft > 0)
            {
                m_Ammo[i] = m_player.m_Weapons[weaponID].m_BulletsLeft;
                m_player.m_Weapons[weaponID].m_BulletsLeft = 0;
            }
            else m_Ammo[i] = 0;
        }
    }
}
