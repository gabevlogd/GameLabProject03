using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartingUI : MonoBehaviour
{
    public Button m_NewGameButton;
    public Button m_CommandsButton;
    public Button m_ExitButton;
    public Button m_BackButton;

    public Image m_CommandsImage;

    public AudioClip m_MainMenuMusic;

    private void Awake()
    {
        if (m_MainMenuMusic != null) SoundManager.Instance.PlayMusic(m_MainMenuMusic);
        m_BackButton.gameObject.SetActive(false);
        m_CommandsImage.gameObject.SetActive(false);

        m_NewGameButton.onClick.AddListener(NewGame);
        m_CommandsButton.onClick.AddListener(ShowCommands);
        m_ExitButton.onClick.AddListener(Exit);
        m_BackButton.onClick.AddListener(Back);
    }

    private void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ShowCommands()
    {
        m_NewGameButton.gameObject.SetActive(false);
        m_CommandsButton.gameObject.SetActive(false);
        m_ExitButton.gameObject.SetActive(false);

        m_BackButton.gameObject.SetActive(true);
        m_CommandsImage.gameObject.SetActive(true);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void Back()
    {
        m_NewGameButton.gameObject.SetActive(true);
        m_CommandsButton.gameObject.SetActive(true);
        m_ExitButton.gameObject.SetActive(true);

        m_BackButton.gameObject.SetActive(false);
        m_CommandsImage.gameObject.SetActive(false);
    }
}
