using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player)) return;
        if (other.TryGetComponent(out EnemyAggro enemy)) Destroy(enemy.gameObject);
        //Debug.Log("OnTriggerEnter");
        Destroy(gameObject);
    }
}
