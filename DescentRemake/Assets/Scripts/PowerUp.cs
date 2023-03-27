using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int m_HealtPointToAdd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats player))
        {
            if (player.m_Stats.m_Healt < player.m_Stats.m_DefaultEnergy)
            {
                if (player.m_Stats.m_DefaultEnergy - player.m_Stats.m_Healt >= m_HealtPointToAdd) player.m_Stats.m_Healt += m_HealtPointToAdd;
                else player.m_Stats.m_Healt = player.m_Stats.m_DefaultEnergy;
                Debug.Log("Life increased");
                Destroy(gameObject);
            }
            else Debug.Log("Healt is already full");
        }
    }
}
