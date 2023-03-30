using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : BasePickUp
{
    protected override void AddToInventory()
    {
        PlayerInventory.m_Instance.m_Keys++;
        HUDManager.m_Instance.ShowMessageOnHUD("COLLECTED KEY");

    }
}
