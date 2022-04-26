using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene("Julien");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
