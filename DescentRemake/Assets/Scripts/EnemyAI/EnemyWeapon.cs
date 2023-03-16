using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : BasicWeapon
{

    EnemyShootingSystem m_shootingSystem;

    protected override void GetInput()
    {
        m_shooting = m_shootingSystem.m_isShooting;
    }

    protected override void Start()
    {
        m_shootingSystem = GetComponentInParent<EnemyShootingSystem>();
        base.Start();
    }

    protected override void Update()
    {
        GetInput();
        if (m_shooting && m_readyToShoot) StartCoroutine(Shoot());
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

        if (m_shooting) StartCoroutine(Shoot());
    }
}
