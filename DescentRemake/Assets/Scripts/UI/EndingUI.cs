using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingUI : MonoBehaviour
{
    public Button m_NewGameButton;
    public Button m_ExitButton;

    public Text m_WinMsg;
    public Text m_LoseMsg;

    private void Awake()
    {
        m_NewGameButton.onClick.AddListener(NewGame);
        m_ExitButton.onClick.AddListener(Exit);

        if (PlayerInventory.m_Instance.m_Healt >= 1) m_WinMsg.gameObject.SetActive(true);
        else m_LoseMsg.gameObject.SetActive(true); 
    }

    private void NewGame()
    {
        Destroy(PlayerInventory.m_Instance.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void Exit()
    {
        Application.Quit();
    }

}
