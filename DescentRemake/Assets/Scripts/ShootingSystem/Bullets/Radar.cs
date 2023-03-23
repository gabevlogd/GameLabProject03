using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Radar : MonoBehaviour
{
    public float m_RadarRadius;

    private PlayerBullet m_bullet;
    private SphereCollider m_collider;
    private bool m_targetAcquired;

    private void Awake()
    {
        m_bullet = GetComponentInParent<PlayerBullet>();
        m_collider = GetComponent<SphereCollider>();
        m_collider.isTrigger = true;

        if (m_bullet.m_BulletType == PlayerBullet.BulletType.Mine) m_collider.radius = m_bullet.m_DamageRange;
        else m_collider.radius = m_RadarRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Detect only enemy, nothing else
        if (!other.TryGetComponent(out EnemyStats enemy)) return;

        //Debug.Log("RadarTrigger");
        RadarBehaviour(other);
  
    }

    /// <summary>
    /// Determines the behaviour of the radar based on the BulletType (HomingMissile or Mine)
    /// </summary>
    /// <param name="target">target to chase or damage</param>
    private void RadarBehaviour(Collider target)
    {
        if (!m_targetAcquired)
        {
            //Debug.Log("Target Acquired");
            m_targetAcquired = true;

            //if bullet type is "Mine" explodes ( AOEDamage() )
            if (m_bullet.m_BulletType == PlayerBullet.BulletType.Mine)
            {
                m_bullet.AOEDamage();
                Destroy(m_bullet.gameObject);
            }
            else //else is a homing missile so gets the target
            {
                m_bullet.m_HomingMissileTarget = target.transform;
            }
        }
    }

}
