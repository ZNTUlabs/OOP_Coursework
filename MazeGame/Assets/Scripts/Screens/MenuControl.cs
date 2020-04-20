using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitPressed()
    {
        Debug.Log("EXIT PRESSED");
        Application.Quit();        
    }

    public void MenuPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
