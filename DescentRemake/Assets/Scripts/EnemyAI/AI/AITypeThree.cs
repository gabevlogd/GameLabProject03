/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * current script's info: class for the movement behaviour of the AI
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeThree : AIMovementBase
{
    protected override void CombatBehaviour() => LookAtPlayer();

    protected override void DetectingBehaviour() => LookAtPlayer();

    protected override void RepositioningBehaviour() => LookAtPlayer();

    private void LookAtPlayer()
    {
        Vector3 direction = m_Player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_AngularSpeed * Time.deltaTime);
    }
}
