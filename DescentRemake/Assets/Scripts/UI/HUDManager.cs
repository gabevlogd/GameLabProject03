using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager m_Instance;
    public Text m_Healt;
    public Text m_Energy;
    public Text m_Score;
    public Text m_Lives;
    public Text m_PrimaryWeapon;
    public Text m_SecondaryWeapon;
    public Text m_SecondaryAmmo;
    public Text m_MessageForPlayer;
    public Text m_Timer;
	public Image m_EnergyBarL;
	public Image m_EnergyBarR;
	public Image m_HealtSprite;
	public Image m_PrimaryWeaponSprite;
	public Image m_SecondaryWeaponSprite;
	public Sprite[] m_HealtSpritesSheet;
	public Sprite[] m_WeaponSpritesSheet;

    private int m_score;
    private int m_lives;
    private float m_energy;
    private float m_healt;
    private BasicWeapon m_lastSecondaryEquiped;
    private BasicWeapon m_lastPrimaryEquiped;
    [HideInInspector] public BasicWeapon m_CurrentSecondaryEquiped;
    [HideInInspector] public BasicWeapon m_CurrentPrimaryEquiped;

    private PlayerInventory m_playerInventory;

    private void Awake()
    {
        m_Instance = this;
        m_playerInventory = PlayerInventory.m_Instance;
        InitializeUI();
    }

    void Update()
    {
        UpdateScoreOnUI();
        UpdateHealtOnUI();
        UpdateEnergyOnUI();
        UpdatePrimaryWeaponOnUI();
        UpdateSecondaryWeaponOnUI();
        UpdateAmmoOnUI();
        UpdateLivesOnUI();
    }

    /// <summary>
    /// Sets the initial stats of the UI
    /// </summary>
    private void InitializeUI()
    {
        m_score = m_playerInventory.m_Score;
        m_healt = m_playerInventory.m_Healt;
        m_energy = m_playerInventory.m_Energy;
        m_Score.text = "Score: " + m_score.ToString();
        m_Healt.text = m_healt.ToString();
        m_Energy.text = m_energy.ToString();

        m_CurrentPrimaryEquiped = m_playerInventory.m_Weapons[m_playerInventory.m_LastPrimaryEquiped.m_WeaponID];
        m_PrimaryWeapon.text = m_CurrentPrimaryEquiped.m_WeaponName;

        m_CurrentSecondaryEquiped = m_playerInventory.m_Weapons[m_playerInventory.m_LastSecondaryEquiped.m_WeaponID];
        m_SecondaryWeapon.text = m_CurrentSecondaryEquiped.m_WeaponName;

    }

    /// <summary>
    /// Update the value of score on UI
    /// </summary>
    private void UpdateScoreOnUI()
    {
        if(m_score != m_playerInventory.m_Score)
        {
            m_score = m_playerInventory.m_Score;
            m_Score.text = "Score: " + m_score.ToString();
        }
    }

    /// <summary>
    /// Updates the value of healt on UI
    /// </summary>
    private void UpdateHealtOnUI()
    {
        if (m_healt != m_playerInventory.m_Healt)
        {
            m_healt = m_playerInventory.m_Healt;
            m_Healt.text = m_healt.ToString();

            int healtLeft = (int)(m_healt / 10f);
            if (healtLeft > 0 && m_healt <= m_playerInventory.m_DefaultHealt) m_HealtSprite.sprite = m_HealtSpritesSheet[healtLeft - 1];
        }
    }

    /// <summary>
    /// Updates the value of lives on UI
    /// </summary>
    private void UpdateLivesOnUI()
    {
        if (m_lives != m_playerInventory.m_Lives)
        {
            m_lives = (int)m_playerInventory.m_Lives;
            m_Lives.text = m_lives.ToString();
        }
    }

    /// <summary>
    /// Update the value of energy on the UI
    /// </summary>
    private void UpdateEnergyOnUI()
    {
        if (m_energy != m_playerInventory.m_Energy)
        {
            m_energy = m_playerInventory.m_Energy;

            int energyLeftInt = (int)m_energy;
            m_Energy.text = energyLeftInt.ToString();
		    m_EnergyBarL.fillAmount = m_energy / 100f;
			m_EnergyBarR.fillAmount = m_energy / 100f;
        }
        
    }

    /// <summary>
    /// Updates the current equipped primary weapon on UI
    /// </summary>
    private void UpdatePrimaryWeaponOnUI()
    {
        if(m_CurrentPrimaryEquiped != m_lastPrimaryEquiped)
        {
            //Debug.Log("Update Primary Info");
            m_PrimaryWeapon.text = m_CurrentPrimaryEquiped.m_WeaponName;
			m_PrimaryWeaponSprite.sprite = m_WeaponSpritesSheet[m_CurrentPrimaryEquiped.m_WeaponID];
            m_lastPrimaryEquiped = m_CurrentPrimaryEquiped;
        }
    }

    /// <summary>
    /// Updates the current equipped secondary weapon on UI
    /// </summary>
    private void UpdateSecondaryWeaponOnUI()
    {
        if (m_CurrentSecondaryEquiped != m_lastSecondaryEquiped)
        {
            //Debug.Log("Update Secondary Info");
            m_SecondaryWeapon.text = m_CurrentSecondaryEquiped.m_WeaponName;
			m_SecondaryWeaponSprite.sprite = m_WeaponSpritesSheet[m_CurrentSecondaryEquiped.m_WeaponID];
			m_lastSecondaryEquiped = m_CurrentSecondaryEquiped;
        }
    }

    /// <summary>
    /// Updates the counter of the current secondary weapon's ammo equipped on UI
    /// </summary>
    private void UpdateAmmoOnUI()
    {
        m_SecondaryAmmo.text = m_CurrentSecondaryEquiped.m_BulletsLeft.ToString();
    }

    /// <summary>
    /// Shows a text on hud
    /// </summary>
    /// <param name="message">text shown</param>
    /// <returns></returns>
    public void ShowMessageOnHUD(string message)
    {
        StopCoroutine(RunHUDMessage(message));
        StartCoroutine(RunHUDMessage(message));
    }
    private IEnumerator RunHUDMessage(string message)
    {
        m_MessageForPlayer.text = message;
        m_MessageForPlayer.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        m_MessageForPlayer.gameObject.SetActive(false);
    }
}