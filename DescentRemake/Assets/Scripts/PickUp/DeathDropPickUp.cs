using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDropPickUp : BasePickUp
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
            //if (m_player.m_Weapons[i].m_MagazineCapacity - m_player.m_Weapons[i].m_BulletsLeft >= m_Ammo[i]) m_player.m_Weapons[i].m_BulletsLeft += m_Ammo[i];
            //else m_player.m_Weapons[i].m_BulletsLeft = m_player.m_Weapons[i].m_MagazineCapacity;
        }
    }

    private void EmptyWeaponAmmo()
    {
        for (int i = 0; i < m_player.m_Weapons.Count; i++)
        {
            if (m_player.m_Weapons[i].m_BulletsLeft > 0)
            {
                m_Ammo[i] = m_player.m_Weapons[i].m_BulletsLeft;
                m_player.m_Weapons[i].m_BulletsLeft = 0;
            }
            else m_Ammo[i] = 0;
        }
    }
}
