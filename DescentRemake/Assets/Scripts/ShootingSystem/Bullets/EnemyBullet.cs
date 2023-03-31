using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BasicBullet
{
    private void Awake()
    {
        if (m_BulletSound != null) SoundManager.Instance.PlayEnemiesSound(m_BulletSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyStats mySelf) || other.TryGetComponent(out BasicBullet bullet)) return;

        //Debug.Log("OnTriggerEnter");
        if (other.TryGetComponent(out PlayerStats player)) player.GetDamage(m_Damage);

        Destroy(gameObject);
    }
}
