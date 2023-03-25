using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : BasePickUp
{
    [Tooltip("Match this ID to the ID of the corresponding weapon")]
    public int m_WeaponID;
    [Tooltip("Number of ammos to increase")]
    public int m_AmmosToAdd;

    protected override void AddToInventory()
    {
        PlayerInventory.m_Instance.m_AmmoCounters[m_WeaponID] += m_AmmosToAdd;
    }
}
