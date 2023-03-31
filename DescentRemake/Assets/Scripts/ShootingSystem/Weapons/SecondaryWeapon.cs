/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : BasicWeapon
{
    public bool m_AllowButtonHold;
    public KeyCode m_KeyToShoot;
    public int m_MagazineCapacity;
    public int m_StartingAmmo;


    protected override void GetInput()
    {
        if (m_AllowButtonHold) m_shooting = Input.GetKey(m_KeyToShoot);
        else m_shooting = Input.GetKeyDown(m_KeyToShoot);
    }

    protected override void Initialize()
    {
        base.Initialize();
        //Debug.Log("initialize: " + m_WeaponName);
        if (m_BulletsLeft == 0) m_BulletsLeft = m_StartingAmmo;

        
    }
}
