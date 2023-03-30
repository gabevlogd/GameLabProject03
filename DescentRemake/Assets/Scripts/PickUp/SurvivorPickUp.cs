using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorPickUp : BasePickUp
{
    protected override void AddToInventory()
    {
        PlayerInventory.m_Instance.m_Survivors++;
        HUDManager.m_Instance.ShowMessageOnHUD("HOSTAGE RECOVERED");
    }

}
