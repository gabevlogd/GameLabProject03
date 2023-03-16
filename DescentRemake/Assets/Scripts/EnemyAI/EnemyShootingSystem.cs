using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingSystem : MonoBehaviour
{

    private EnemyAggro m_enemyAggro;
    public bool m_isShooting;

    // Start is called before the first frame update
    void Start()
    {
        m_enemyAggro = GetComponent<EnemyAggro>();
        m_isShooting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_enemyAggro.m_isAggro)
        {
            FindPlayer();   //Method used to check if a raycast hit the player
        }
        else
            m_isShooting = false;
    }

    public void FindPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if(hit.collider?.tag == "Player")
        {
            m_isShooting = true;
            Debug.Log("Shooting");
        }
    }
}
