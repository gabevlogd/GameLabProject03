using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartingUI : MonoBehaviour
{
    public GameObject m_MainTab;

    public Button m_NewGameButton;
    public Button m_CommandsButton;
    public Button m_CreditsButton;
    public Button m_ExitButton;
    public Button m_BackButton;

    public Image m_CommandsImage;
    public Image m_CreditsImage;

    public AudioClip m_MainMenuMusic;

    private void Awake()
    {
        if (m_MainMenuMusic != null) SoundManager.Instance.PlayMusic(m_MainMenuMusic);
        m_BackButton.gameObject.SetActive(false);
        m_CommandsImage.gameObject.SetActive(false);
        m_CreditsImage.gameObject.SetActive(false);

        m_NewGameButton.onClick.AddListener(NewGame);
        m_CommandsButton.onClick.AddListener(() => ShowOrHideTab(m_CommandsImage));
        m_CreditsButton.onClick.AddListener(() => ShowOrHideTab(m_CreditsImage));
        m_ExitButton.onClick.AddListener(Exit);
        m_BackButton.onClick.AddListener(Back);
    }

    private void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ShowOrHideTab(Image tabToShowOrHide)
    {
        m_MainTab.gameObject.SetActive(m_MainTab.gameObject.activeInHierarchy ^ true);
        m_BackButton.gameObject.SetActive(m_BackButton.gameObject.activeInHierarchy ^ true);
        tabToShowOrHide.gameObject.SetActive(tabToShowOrHide.gameObject.activeInHierarchy ^ true);
    }


    private void Exit()
    {
        Application.Quit();
    }

    private void Back()
    {
        m_MainTab.gameObject.SetActive(m_MainTab.gameObject.activeInHierarchy ^ true);
        m_BackButton.gameObject.SetActive(m_BackButton.gameObject.activeInHierarchy ^ true);

        if (m_CreditsImage.gameObject.activeInHierarchy) m_CreditsImage.gameObject.SetActive(m_CreditsImage.gameObject.activeInHierarchy ^ true);
        else m_CommandsImage.gameObject.SetActive(m_CommandsImage.gameObject.activeInHierarchy ^ true);
    }
}
