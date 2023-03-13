using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingSystem : MonoBehaviour
{

    private EnemyAggro m_enemyAggro;

    // Start is called before the first frame update
    void Start()
    {
        m_enemyAggro = GetComponent<EnemyAggro>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_enemyAggro.m_isAggro)
        {
            FindPlayer();   //Method used to check if a raycast hit the player
        }
    }

    public void FindPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if(hit.collider.tag == "Player")
        {
            Debug.Log("Shooting");
        }
    }
}
