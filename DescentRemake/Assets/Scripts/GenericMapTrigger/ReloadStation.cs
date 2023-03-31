/*
 * GameLab 2022/2023
 * first project: Descent's remake
 * current script's info: class for reloading player energy when collides with reload station
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadStation : MonoBehaviour
{
    public int m_EnergyToReload;
    public float m_TimeBetweenReload;

    public AudioClip m_ReloadingSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerStats player)) return;
        if (m_ReloadingSound != null) SoundManager.Instance.PlayWorldSound(m_ReloadingSound);
        StartCoroutine(ReloadEnergy());
    }

    private void OnTriggerExit(Collider other)
    {

        if (!other.TryGetComponent(out PlayerStats player)) return;
        if (m_ReloadingSound != null) SoundManager.Instance.WorldEffectsSource.Stop();
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
