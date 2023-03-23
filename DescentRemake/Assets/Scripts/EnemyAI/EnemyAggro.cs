using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    [Range(0,100)]
    public float m_DetectionDistance;       //The max range of the Spherecast used to see if the player is detected
    [Range(0,100)]
    public float m_DistanceFromPlayer;     //Set this to make the enemy not collide to the player, it's the distance beetween them

    public float m_MaxSpeed;                //Max Speed that this enemy can reach
    public float m_Accelleration;           //To set the accelleration when this object moves
    public float m_MaxRotationSpeed;        //Max Speed of its rotation
    public float m_AngularAccelleration;    //How fast his accelleration is when it rotate
    
    [System.NonSerialized]
    public Transform m_player;             //We need the transform of the player if this enemy find it

    public bool m_isAggro;
    public float m_actualAngularSpeed;      //Actual speed of the Enemy rotation
    public float m_actualSpeed;             //Actual speed of the Enemy


    //Combat Mode
    public float m_InnerCombatDistance; //Min distance needed for the enemy to enter combat mode
    public float m_OuterCombatDistance; //Max Distance needed for the enemy to enter combat mode

    public int m_TypeOfEnemy;   //Every enemy can do something unique

    private float m_distanceBeetweenEnemyPlayer;  //We need to save the distance beetween Enemy and Player
    private bool m_isInCombat;  //Check if the enemy is in combat

    public float m_MovementInCombatInSeconds;   //The cooldown of his different type of movement while in combat mode
    private float m_cooldownTimer;  //Used to create the cooldown system
    private int m_randomMovement;   //Used to see where to move random

    //Test
    public Rigidbody m_Rigidbody;
    void Start()
    {
        m_actualSpeed = 0;
        m_cooldownTimer = 0;
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_isAggro = PlayerDetection();  //See if the Player is detected and if is Aggroed
        m_isInCombat = CombatDetection();

        if (m_isAggro)  //If the player is detected and we are not in fight range we can move
        {
            EnemyRotation();
        }
        if (m_isAggro && m_isInCombat)
        {
            EnemyCombatMode(m_TypeOfEnemy);
        }
        else if(m_isAggro && !m_isInCombat)
            EnemyMovement();

        //Debug.Log("Time is: " + Time.deltaTime);
    }

    public bool PlayerDetection()   //Aggro System of this enemy
    {
        int layerMask = 1 << 7;
        Collider[] m_CollidersInRange = Physics.OverlapSphere(transform.position, m_DetectionDistance, layerMask);  //This give you every collider in a sphere range
        /*
            We can optimize this using a specific Layer for the Player so that we don't have to check every Collider in the game, but just the one in the set layer
        */

        if (m_CollidersInRange.Length == 0)
        {
            m_actualSpeed = 0;
            m_actualAngularSpeed = 0;
            return false;    //If nothing is Detected return Null
        }

        foreach(Collider collider in m_CollidersInRange)
        {
            if (collider.tag == "Player")  //If the player is Detected
            {
                //Debug.Log("Player Detected");
                m_player = collider.transform; //I can get the player info

                if (Vector3.Distance(transform.position, m_player.position) > m_DistanceFromPlayer)    //If we are not in shooting range we can return true
                {
                    return true;
                }
                else
                {
                    //Debug.Log("I'm near Player");
                    return false;
                }
            }
        }

        


        m_actualSpeed = 0;
        m_actualAngularSpeed = 0;
        return false;   //If a Player is not detected return false
    }

    public void EnemyMovement()
    {
        //Debug.Log("Moving Toward Player");
        if (m_actualSpeed < m_MaxSpeed)
        {
            m_actualSpeed += m_Accelleration;
        }
        else
        {
            m_actualSpeed = m_MaxSpeed;
        }
        transform.position = Vector3.MoveTowards(transform.position, m_player.position, m_actualSpeed);
    }

    public void EnemyRotation()
    {
        //Debug.Log("Rotating");
        if(m_actualAngularSpeed < m_MaxRotationSpeed)
        {
            m_actualAngularSpeed += m_AngularAccelleration / 100;
        }
        else
        {
            m_actualAngularSpeed = m_MaxRotationSpeed;
        }
        //Rotating the enemy towards the player
        Quaternion lookRotation = Quaternion.LookRotation(m_player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, m_actualAngularSpeed);
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

    //Combat Mode
    public bool CombatDetection()
    {
        if(m_player != null)
        {
            m_distanceBeetweenEnemyPlayer = Vector3.Distance(transform.position, m_player.position);
            if (m_distanceBeetweenEnemyPlayer <= m_OuterCombatDistance && m_distanceBeetweenEnemyPlayer >= m_InnerCombatDistance)
            {
                return true;
            }
        }
        return false;
    }

    public void EnemyCombatMode(int enemyType)  //0 - Enemy Type 1, 1 - EnemyType2, Ecc...
    {
        if (m_actualSpeed < m_MaxSpeed)
        {
            m_actualSpeed += m_Accelleration;
        }
        else
        {
            m_actualSpeed = m_MaxSpeed;
        }
        switch (enemyType)
        {
            case 0:
                if (m_cooldownTimer == 0)
                {
                    System.Random rng = new System.Random();
                    m_randomMovement = rng.Next(0,4);
                }
                if (m_randomMovement == 0)
                {
                    //Move right
                    transform.Translate(Vector3.right * m_actualSpeed);
                    Debug.Log("Moving right");
                }
                    
                else if (m_randomMovement == 1)
                {
                    //move left
                    transform.Translate(Vector3.left * m_actualSpeed);
                    Debug.Log("Moving left");
                }
                else if (m_randomMovement == 2)
                {
                    //Move up
                    transform.Translate(transform.up * m_actualSpeed);
                    Debug.Log("Moving Up");
                }
                else
                {
                    //Move down
                    transform.Translate(-transform.up * m_actualSpeed);

                    Debug.Log("Moving down");
                }
                break;
            case 1:
                if (m_cooldownTimer == 0)
                {
                    System.Random rng = new System.Random();
                    m_randomMovement = rng.Next(0,2);
                }
                if (m_randomMovement == 0)
                {
                    //Move right
                    transform.Translate(Vector3.up * m_actualSpeed);
                    Debug.Log("Moving up");
                }
                else if (m_randomMovement == 1)
                {
                    //move left
                    transform.Translate(Vector3.down * m_actualSpeed);
                    Debug.Log("Moving down");
                }
                break;
            case 2:
                Debug.Log("Doing absolutely nothing");
                break;
            default: break;
        }
        m_cooldownTimer += Time.deltaTime;
        if(m_cooldownTimer >= m_MovementInCombatInSeconds)
        {
            m_cooldownTimer = 0;
        }
        //Debug.Log("Cooldown Timer: " + m_cooldownTimer);
    }
}
