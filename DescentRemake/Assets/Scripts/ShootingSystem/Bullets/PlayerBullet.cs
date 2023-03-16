using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BasicBullet
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player) || other.TryGetComponent(out BasicBullet bullet)) return;

        if (other.TryGetComponent(out EnemyStats enemy)) enemy.GetDamage(m_Damage);
        
        Debug.Log("OnTriggerEnter");
        Destroy(gameObject);
    }
}
