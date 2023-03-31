using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypeBoss : AIMovementBase
{
    public BasicWeapon m_Weapon;
    public Transform[] m_WeaponSlots;

    protected override void CombatBehaviour()
    {
        Debug.Log("CombatState");
        m_Weapon.transform.position = ClosestSlotToPlayer();
    }

    private Vector3 ClosestSlotToPlayer()
    {
        Transform closestSlot = m_WeaponSlots[0];
        float distanceFromPlayer = Vector3.Distance(m_Player.position, m_WeaponSlots[0].position);
        for (int i = 1; i<4; i++)
        {
            if (Vector3.Distance(m_Player.position, m_WeaponSlots[i].position) < distanceFromPlayer)
            {
                distanceFromPlayer = Vector3.Distance(m_Player.position, m_WeaponSlots[i].position);
                closestSlot = m_WeaponSlots[i];
            }
        }
        return closestSlot.position;
    }
}
