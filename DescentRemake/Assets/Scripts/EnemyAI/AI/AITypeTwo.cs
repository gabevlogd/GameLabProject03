/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * current script's info: class for the movement behaviour of the AI
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeTwo : AITypeOne
{

    protected override Vector3 WorldAxis()
    {
        if (Mathf.Abs(transform.position.z - m_Player.position.z) >= m_InnerCombatDistance) //if the enemy is on the z axis of the world
        {
            return Vector3.right;
        }
        if (Mathf.Abs(transform.position.x - m_Player.position.x) >= m_InnerCombatDistance) //if the enemy is on the x axis of the world
        {
            return Vector3.forward;
        }
        if (Mathf.Abs(transform.position.y - m_Player.position.y) >= m_InnerCombatDistance) //if the enemy is on the y axis of the world
        {
            if (m_randomAxis.x != 0) return Vector3.forward;
            else return Vector3.right;
        }

        return Vector3.up;
    }

}
