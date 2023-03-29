using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIMovementBase : MonoBehaviour
{
    public Transform m_Player;

    public float m_DetectionDistance, m_OuterCombatDistance, m_InnerCombatDistance;
    public float m_Speed, m_AngularSpeed;
    [Tooltip("the minimum time after which the enemy can change direction")]
    public float m_ChangeDirectionTimer;
    public bool m_PlayerInSight { get; set; }

    protected float m_distanceFromPlayer;
    protected float m_changeDirectionTimer;

    protected enum MovementState { idle, detecting, combat, repositioning };
    protected MovementState m_movementState;
    protected MovementState m_lastMovementState;

    protected delegate void MovementBehaviour();
    protected MovementBehaviour m_movementBehaviour;

    protected void Awake()
    {
        m_Player = FindObjectOfType<PlayerMovement>().transform;
        m_changeDirectionTimer = m_ChangeDirectionTimer;
        m_movementBehaviour = IdleBehaviour;
    }

    protected void Update()
    {
        UpdateMovementState();
        UpdateMovementBehaviour();
        m_movementBehaviour();
    }



    /// <summary>
    /// Updates the state of the movement based on the distance from player
    /// </summary>
    protected void UpdateMovementState()
    {
        m_distanceFromPlayer = Vector3.Distance(transform.position, m_Player.position);

        if (m_distanceFromPlayer > m_DetectionDistance) m_movementState = MovementState.idle;
        else if (m_OuterCombatDistance < m_distanceFromPlayer && m_distanceFromPlayer <= m_DetectionDistance) m_movementState = MovementState.detecting;
        else if (m_InnerCombatDistance <= m_distanceFromPlayer && m_distanceFromPlayer <= m_OuterCombatDistance) m_movementState = MovementState.combat;
        else if (m_distanceFromPlayer < m_InnerCombatDistance) m_movementState = MovementState.repositioning;
    }

    /// <summary>
    /// Updates the the movement behaviour based on the movement state
    /// </summary>
    protected void UpdateMovementBehaviour()
    {
        if (m_movementState != m_lastMovementState)
        {
            switch (m_movementState)
            {
                case MovementState.idle:
                    m_movementBehaviour = IdleBehaviour;
                    break;
                case MovementState.detecting:
                    m_movementBehaviour = DetectingBehaviour;
                    break;
                case MovementState.combat:
                    m_movementBehaviour = CombatBehaviour;
                    break;
                case MovementState.repositioning:
                    m_movementBehaviour = RepositioningBehaviour;
                    break;
            }

            m_lastMovementState = m_movementState;
        }
    }


    protected virtual void IdleBehaviour()
    {
        Debug.Log("idle behaviour");
    }

    /// <summary>
    /// Makes the enemy go away from player to recover the combat distance
    /// </summary>
    protected virtual void DetectingBehaviour()
    {
        Debug.Log("detecting behaviour");
    }

    /// <summary>
    /// Makes the enemy follow a specific movement pattern around the player
    /// </summary>
    protected abstract void CombatBehaviour();

    /// <summary>
    /// Makes the enemy returns to target combat distance from player
    /// </summary>
    protected virtual void RepositioningBehaviour()
    {
        Debug.Log("repositioning behaviour");
    }
}
