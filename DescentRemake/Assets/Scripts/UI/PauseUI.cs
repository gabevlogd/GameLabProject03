using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject m_Menu;
    public Button m_NewGameButton;
    public Button m_ExitButton;

    public Slider m_MouseX;
    public Slider m_MouseY;

    public Text m_SliderValueX;
    public Text m_SliderValueY;

    private void Awake()
    {
        m_NewGameButton.onClick.AddListener(NewGame);
        m_ExitButton.onClick.AddListener(Exit);

        m_MouseX.value = 400;
        m_MouseY.value = 200;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CheckMenu();
        UpdateMouseSens();
    }

    private void NewGame()
    {
        Destroy(PlayerInventory.m_Instance.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void CheckMenu()
    {
        if (m_Menu.gameObject.activeInHierarchy)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            m_Menu.gameObject.SetActive(false);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            m_Menu.gameObject.SetActive(true);
        }
    }

    private void UpdateMouseSens()
    {
        if (PlayerMovement.m_SensX != m_MouseX.value || PlayerMovement.m_SensY != m_MouseY.value)
        {
            PlayerMovement.m_SensX = m_MouseY.value;
            PlayerMovement.m_SensY = m_MouseX.value;
            m_SliderValueX.text = "SENS MOUSE X: " + m_MouseX.value;
            m_SliderValueY.text = "SENS MOUSE Y: " + m_MouseY.value;
        }
    }
}
