using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : EnemyWeapon
{
    protected override void SimultaneousSpawn()
    {
        BasicBullet Bullet = Instantiate(m_Bullet, m_firePoints[m_indexForDelegate].position, m_firePoints[m_indexForDelegate].rotation);
        Vector3 bulletDirection = m_movementBaseData.m_Player.position - transform.position;
        Bullet.GetComponent<Rigidbody>().velocity = bulletDirection.normalized * m_BulletSpeed;
        Bullet.m_ShotedFromID = this.m_WeaponID;

        UpdateDelegateIndex();
    }
}
