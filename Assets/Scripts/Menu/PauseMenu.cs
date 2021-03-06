using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    bool pauseMenuIsOpen = false;
    public bool settingsMenuIsOpen = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenuIsOpen)
                pauseMenuIsOpen = false;
            else
                pauseMenuIsOpen = true;
        }

        if (pauseMenuIsOpen)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        if(pauseMenuIsOpen)
            pauseMenuIsOpen=false;
    }

    public void Options()
    {
        settingsMenu.SetActive(true);
        settingsMenuIsOpen = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
