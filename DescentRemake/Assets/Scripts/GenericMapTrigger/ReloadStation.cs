using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadStation : MonoBehaviour
{
    public int m_EnergyToReload;
    public float m_TimeBetweenReload;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerStats player)) return;
        StartCoroutine(ReloadEnergy());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out PlayerStats player)) return;
        StopAllCoroutines();
    }

    private IEnumerator ReloadEnergy()
    {
        if (PlayerInventory.m_Instance.m_Energy < PlayerInventory.m_Instance.m_MaxEnergy) PlayerInventory.m_Instance.m_Energy += m_EnergyToReload;
        else StopAllCoroutines();
        yield return new WaitForSeconds(m_TimeBetweenReload);
        StartCoroutine(ReloadEnergy());
    }
}
