using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : BasicWeapon
{
    public bool m_AllowButtonHold;
    public KeyCode m_KeyToShoot;
    public int m_MagazineCapacity;

    protected override void GetInput()
    {
        if (m_AllowButtonHold) m_shooting = Input.GetKey(m_KeyToShoot);
        else m_shooting = Input.GetKeyDown(m_KeyToShoot);
    }

    protected override void Initialize()
    {
        base.Initialize();
        m_BulletsLeft = m_MagazineCapacity;
    }
}
