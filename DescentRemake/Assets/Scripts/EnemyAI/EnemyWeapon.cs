using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : BasicWeapon
{

    AIMovementBase m_movementBaseData;


    protected override void Start()
    {
        m_movementBaseData = GetComponentInParent<AIMovementBase>();
        base.Start();
    }

    protected override void Update()
    {
        GetInput();
        if (m_shooting && m_readyToShoot) StartCoroutine(Shoot());
    }

    protected override void GetInput()
    {
        m_shooting = PlayerInSight();
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

    private bool PlayerInSight()
    {
        Vector3 direction = m_movementBaseData.m_Player.position - transform.position;
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, direction, out hitInfo, m_movementBaseData.m_DetectionDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.transform == m_movementBaseData.m_Player.transform) return true;

        return false;

    }

}
