/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * current script's info: class for breaking the TvSreen(prop) when hitted by a bullet
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvScreen : MonoBehaviour
{
    public SpriteRenderer m_ScreenSprite;
    public SpriteRenderer m_BorekenScreenSprite;

    public AudioClip m_BreakingSound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.TryGetComponent(out BasicBullet bullet))
        {
            if (m_ScreenSprite.sortingOrder < m_BorekenScreenSprite.sortingOrder) return;
            else
            {
                if (m_BreakingSound != null) SoundManager.Instance.PlayWorldSound(m_BreakingSound);
                int sortingOrder = m_ScreenSprite.sortingOrder;
                m_ScreenSprite.sortingOrder = m_BorekenScreenSprite.sortingOrder;
                m_BorekenScreenSprite.sortingOrder = sortingOrder;
            }
        }
    }
}
