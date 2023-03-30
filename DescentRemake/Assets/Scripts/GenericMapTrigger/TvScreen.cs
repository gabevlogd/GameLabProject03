using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvScreen : MonoBehaviour
{
    public SpriteRenderer m_ScreenSprite;
    public SpriteRenderer m_BorekenScreenSprite;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.TryGetComponent(out BasicBullet bullet))
        {
            if (m_ScreenSprite.sortingOrder < m_BorekenScreenSprite.sortingOrder) return;
            else
            {
                int sortingOrder = m_ScreenSprite.sortingOrder;
                m_ScreenSprite.sortingOrder = m_BorekenScreenSprite.sortingOrder;
                m_BorekenScreenSprite.sortingOrder = sortingOrder;
            }
        }
    }
}
