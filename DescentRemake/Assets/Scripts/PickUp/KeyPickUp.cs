/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : BasePickUp
{
    protected override void AddToInventory()
    {
        PlayerInventory.m_Instance.m_Keys++;
        if (m_PickUpSound != null) SoundManager.Instance.PlayWorldSound(m_PickUpSound);
        HUDManager.m_Instance.ShowMessageOnHUD("COLLECTED KEY");

    }
}
