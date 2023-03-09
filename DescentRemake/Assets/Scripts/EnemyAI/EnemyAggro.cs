using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    [Range(0,100)]
    public float m_DetectionDistance;       //The max range of the Spherecast used to see if the player is detected

    public float m_InnerCombatDistance;     //Min Range to see if you can shoot
    public float m_OuterCombatDistance;     //Max Range to see if you can shoot

    public float m_MaxSpeed;                //Max Speed that this enemy can reach
    public float m_Accelleration;           //To set the accelleration when this object moves
    public float m_MaxRotationSpeed;        //Max Speed of its rotation
    public float m_AngularAccelleration;    //How fast his accelleration is when it rotate

    private Transform m_player;             //We need the transform of the player if this enemy find it
    private bool m_isAggro;
    void Start()
    {
    }

    void FixedUpdate()
    {
        m_isAggro = PlayerDetection();  //See if the Player is detected and if is Aggroed

        if (m_isAggro)  //If the player is detected and we are not in fight range we can move
            EnemyMovement();
    }


    public bool PlayerDetection()   //Aggro System of this enemy
    {
        Collider[] m_CollidersInRange = Physics.OverlapSphere(transform.position, m_DetectionDistance);  //This give you every collider in a sphere range
        /*
            We can optimize this using a specific Layer for the Player so that we don't have to check every Collider in the game, but just the one in the set layer
        */

        if (m_CollidersInRange.Length == 0) return false;    //If nothing is Detected return Null

        foreach(var m_ColliderInRange in m_CollidersInRange)
        {
            if (m_ColliderInRange.tag == "Player")  //If the player is Detected
            {
                Debug.Log("Player Detected");
                m_player = m_ColliderInRange.transform; //I can get the player info

                if (Vector3.Distance(transform.position, m_player.position) > m_OuterCombatDistance)    //If we are not in shooting range we can return true
                {
                    Debug.Log("Moving Toward Player");
                    return true;
                }
                else
                {
                    Debug.Log("Shooting Player");
                    return false;
                }
            }
        }
        return false;   //If a Player is not detected return false
    }

    public void EnemyMovement()
    {
        Debug.Log("Eccomi");
        transform.position = Vector3.MoveTowards(transform.position, m_player.position, m_MaxSpeed * Time.deltaTime);
        transform.LookAt(m_player.transform);
    }
}
