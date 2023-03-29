using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeOne : AIMovementBase
{
    public float m_RotateAroundPlayerSpeed;
    protected Vector3 m_randomAxis = Vector3.up;

    protected void OnCollisionEnter(Collision collision)
    {
        m_randomAxis *= -1;
    }

    protected override void DetectingBehaviour()
    {
        base.DetectingBehaviour();

        Vector3 direction = m_Player.position - transform.position;
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, direction, out hitInfo, m_DetectionDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.transform == m_Player.transform)
        {
            //Debug.Log("player in sight");

            transform.position = Vector3.MoveTowards(transform.position, m_Player.position, m_Speed * Time.deltaTime);

            //Look at player - start
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_AngularSpeed * Time.deltaTime);
            //Look at player - end
        }
    }

    protected override void CombatBehaviour()
    {
        Debug.Log("combat behaviour");

        //Look at player - start
        Vector3 direction = m_Player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_AngularSpeed * Time.deltaTime);
        //Look at player - end

        CombatMovementPattern();
    }


    protected override void RepositioningBehaviour()
    {
        base.RepositioningBehaviour();
        
        //Look at player - start
        Vector3 direction = m_Player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_AngularSpeed * Time.deltaTime);
        //Look at player - end

        transform.position = Vector3.MoveTowards(transform.position, m_Player.position, -m_Speed * Time.deltaTime); //goes away from player 
    }

    /// <summary>
    /// The movement pattern of this type of enemy
    /// </summary>
    protected virtual void CombatMovementPattern()
    {
        //Rotate around player randomly - start
        if (ChangeDirectionCondition())
        {
            m_randomAxis = WorldAxis() * RandomSign();
            //Debug.Log("ChangeDirection: " + m_randomAxis);
        }
        else transform.RotateAround(m_Player.position, m_randomAxis, m_RotateAroundPlayerSpeed * Time.deltaTime);
        //Rotate around player randomly - end


        /// <summary>
        /// NOTE: not the correct model written on GDD but similar and better performing
        /// </summary>
        /// 
        //if (m_changeDirectionTimer > 0f)
        //{
        //    m_changeDirectionTimer -= Time.deltaTime;
        //    transform.RotateAround(m_Player.position, m_randomAxis.normalized, 20f * Time.deltaTime);
        //}
        //else
        //{
        //    m_changeDirectionTimer = m_ChangeDirectionTimer;
        //    m_randomAxis = new Vector3(Random.value, Random.value, Random.value);
        //}
    }


    /// <summary>
    /// Returns a world axis based on enemy position
    /// </summary>
    /// <returns>World axis</returns>
    protected virtual Vector3 WorldAxis()
    {
        if (Mathf.Abs(transform.position.z - m_Player.position.z) >= m_InnerCombatDistance) //if the enemy is on the z axis of the world
        {
            //can return only x and y axis
            if (m_randomAxis.y != 0) return Vector3.right;
            else return Vector3.up;
        }
        if (Mathf.Abs(transform.position.x - m_Player.position.x) >= m_InnerCombatDistance) //if the enemy is on the x axis of the world
        {
            //can return only z and y axis
            if (m_randomAxis.y != 0) return Vector3.forward;
            else return Vector3.up;
        }
        if (Mathf.Abs(transform.position.y - m_Player.position.y) >= m_InnerCombatDistance) //if the enemy is on the y axis of the world
        {
            //can return only x and z axis
            if (m_randomAxis.x != 0) return Vector3.forward;
            else return Vector3.right;
        }

        return Vector3.up;
    }

    /// <summary>
    /// </summary>
    /// <returns>true if the enemy is in a legal positions to change movement direction</returns>
    protected virtual bool ChangeDirectionCondition()
    {

        if(m_changeDirectionTimer > 0)
        {
            m_changeDirectionTimer -= Time.deltaTime;
            return false;
        }

        if (m_randomAxis.y != 0) //if the rotating axis is Y
        {
            //change direction only if the enemy is on the Z or X axis
            if(Mathf.Abs(transform.position.x - m_Player.position.x) < 0.3f || Mathf.Abs(transform.position.z - m_Player.position.z) < 0.3f)
            {
                m_changeDirectionTimer = m_ChangeDirectionTimer;
                return true;
            }
        }
        if (m_randomAxis.x != 0) //if the rotating axis is X
        {
            //change direction only if the enemy is on the Z or Y axis
            if (Mathf.Abs(transform.position.y - m_Player.position.y) < 0.3f || Mathf.Abs(transform.position.z - m_Player.position.z) < 0.3f)
            {
                m_changeDirectionTimer = m_ChangeDirectionTimer;
                return true;
            }
        }
        if (m_randomAxis.z != 0) //if the rotating axis is Z
        {
            //change direction only if the enemy is on the Y or X axis
            if (Mathf.Abs(transform.position.x - m_Player.position.x) < 0.3f || Mathf.Abs(transform.position.y - m_Player.position.y) < 0.3f)
            {
                m_changeDirectionTimer = m_ChangeDirectionTimer;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// </summary>
    /// <returns>return randomly 1 or -1</returns>
    protected int RandomSign()
    {
        return Random.value < .5 ? 1 : -1;
    }


}
