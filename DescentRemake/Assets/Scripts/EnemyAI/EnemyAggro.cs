using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{

    public float m_DetectionDistance;   //The max range of the Spherecast used to see if the player is detected

    private bool m_isAggro;             //Check if something is hit by the Spherecast
    private Transform player;           //We need the transform of the player if this enemy find him

    void Start()
    {
        m_isAggro = false;
    }

    void Update()
    {
        PlayerDetection();
    }

    public Transform PlayerDetection()
    {
        Collider[] m_CollidersInRange = Physics.OverlapSphere(transform.position, m_DetectionDistance);  //This give you every collider in a sphere range
        /*
            We can optimize this using a specific Layer for the Player so that we don't have to check every Collider in the game
         */


        if (m_CollidersInRange.Length == 0) return null;    //If nothing is Detected return Null

        foreach(var m_ColliderInRange in m_CollidersInRange)
        {
            if (m_ColliderInRange.tag == "Player")  //If the player is Detected return his Transform
            {
                Debug.Log("Preso");
                m_isAggro = true;                   //m_isAggro is true because we got our target
                return m_ColliderInRange.transform;
            }
            else
                m_isAggro = false;
        }

        return null;
    }
}
