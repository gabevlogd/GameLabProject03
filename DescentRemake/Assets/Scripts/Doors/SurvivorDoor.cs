/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorDoor : MonoBehaviour
{
    public SpriteRenderer[] m_DoorSprites;
    private DoorBehaviours m_thisDoor;
    private int m_lastUpdatedHealt;
    private int m_arrayIndex;

    private void Awake()
    {
        m_thisDoor = GetComponent<DoorBehaviours>();
        m_lastUpdatedHealt = m_thisDoor.m_Healt;
        m_arrayIndex = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        UpdateSprite();
    }

    private void OnTriggerEnter(Collider other)
    {
        UpdateSprite();
    }


    private void UpdateSprite()
    {
        if ( Mathf.Abs(m_lastUpdatedHealt - m_thisDoor.m_Healt) >= 25f)
        {
            m_lastUpdatedHealt = m_thisDoor.m_Healt;
            m_DoorSprites[m_arrayIndex - 1].gameObject.SetActive(false); 
            if (m_arrayIndex < 4) m_DoorSprites[m_arrayIndex].gameObject.SetActive(true);
            m_arrayIndex++;

        }
    }




}
