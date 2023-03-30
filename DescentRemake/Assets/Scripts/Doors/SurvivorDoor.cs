using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorDoor : MonoBehaviour
{
    public Sprite[] m_DoorSprites;
    private DoorBehaviours m_thisDoor;
    private int m_lastUpdatedHealt;

    private void Awake()
    {
        m_thisDoor = GetComponent<DoorBehaviours>();
        m_lastUpdatedHealt = m_thisDoor.m_Healt;
    }

    //OnColl




}
