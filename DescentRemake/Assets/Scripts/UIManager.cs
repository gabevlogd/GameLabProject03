using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager m_Instance;
    public Text m_Energy;

    private float m_energyLeft;

    private void Awake()
    {
        m_Instance = this;
        InitializeUI();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        UpdateEnergyOnUI();
    }

    private void InitializeUI()
    {
        m_energyLeft = PlayerInventory.m_Instance.m_Energy;
        m_Energy.text = "Energy: " + m_energyLeft.ToString();
    }


    /// <summary>
    /// Update the value of energy on the UI
    /// </summary>
    private void UpdateEnergyOnUI()
    {
        if (m_energyLeft != PlayerInventory.m_Instance.m_Energy)
        {
            m_energyLeft = PlayerInventory.m_Instance.m_Energy;

            int energyLeftInt = (int)m_energyLeft;
            float energyLeftDecimalPart = m_energyLeft - energyLeftInt;

            if (Mathf.Approximately(energyLeftDecimalPart, 0f)) m_Energy.text = "Energy: " + m_energyLeft.ToString();
        }
        
    }
}
