/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrimaryWeapon : BasicWeapon
{

    public float m_EnergyCost;
    public bool m_AllowButtonHold;
    public KeyCode m_KeyToShoot;

    protected override void Update()
    {
        GetInput();
        if (m_shooting && m_readyToShoot && PlayerInventory.m_Instance.m_Energy > 0) StartCoroutine(Shoot());
    }

    protected override void GetInput()
    {
        if (m_AllowButtonHold) m_shooting = Input.GetKey(m_KeyToShoot);
        else m_shooting = Input.GetKeyDown(m_KeyToShoot);
    }

    protected override IEnumerator Shoot()
    {
        m_readyToShoot = false;

        SpawnBullets();

        if (m_allowInvoke)
        {
            Invoke("ResetShoot", m_TimeBetweenShots);
            m_allowInvoke = false;
        }

        yield return new WaitUntil(() => m_readyToShoot);

        if (m_shooting && PlayerInventory.m_Instance.m_Energy > 0) StartCoroutine(Shoot());
    }

    protected override void SpawnBullets()
    {
        m_bulletsShots++;
        PlayerInventory.m_Instance.m_Energy -= m_EnergyCost;

        m_spawnTypeHandler();

        if (m_firePoints.Length > 1) RotateFirePoints();

        if (m_bulletsShots < m_BulletsPerShot && !m_readyToShoot && m_FireType == FireType.Simultaneous) Invoke("SpawnBullets", m_TimeBetweenBullets);
    }
}
