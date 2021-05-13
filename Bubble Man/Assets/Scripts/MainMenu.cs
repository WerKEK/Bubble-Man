using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Contune()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Save"));
            
        }
    }
    public void DelSaves()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Exit()
    {
        Application.Quit();
    }
}

