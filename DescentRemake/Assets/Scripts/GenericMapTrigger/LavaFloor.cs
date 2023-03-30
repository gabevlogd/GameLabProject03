using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloor : MonoBehaviour
{
    public float m_TimeBetweenDamage;
    public int m_Damage;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.TryGetComponent(out PlayerStats player)) StartCoroutine(LavaDamage());
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("exit");
        if (collision.gameObject.TryGetComponent(out PlayerStats player)) StopAllCoroutines();
    }

    private IEnumerator LavaDamage()
    {
        PlayerInventory.m_Instance.m_Healt -= m_Damage;
        yield return new WaitForSeconds(m_TimeBetweenDamage);
        StartCoroutine(LavaDamage());
    }
}
