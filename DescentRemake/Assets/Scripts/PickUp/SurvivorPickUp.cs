using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorPickUp : BasePickUp
{
    public int m_ScoreToAdd;

    protected override void AddToInventory()
    {
        PlayerInventory.m_Instance.m_Score += m_ScoreToAdd;
        HUDManager.m_Instance.ShowMessageOnHUD("HOSTAGE RECOVERED");
    }

}
