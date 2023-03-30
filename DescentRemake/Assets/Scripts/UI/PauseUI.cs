using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
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

        m_MouseX.value = PlayerMovement.m_SensX;
        m_MouseY.value = PlayerMovement.m_SensY;

    }

    private void Update()
    {
        UpdateMouseSens();
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
