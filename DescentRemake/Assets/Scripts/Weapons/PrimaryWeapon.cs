using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrimaryWeapon : BasicWeapon
{

    public bool m_AllowButtonHold;

    protected override void Initialize()
    {
        base.Initialize();
        m_bulletsLeft = (int)PlayerInventory.m_Instance.m_Energy;
    }

    protected override void GetInput()
    {
        if (m_AllowButtonHold) m_shooting = Input.GetKey(m_KeyToShoot);
        else m_shooting = Input.GetKeyDown(m_KeyToShoot);
    }
}
