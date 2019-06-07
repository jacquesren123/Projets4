using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Multi()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void TestProp()
    {
        SceneManager.LoadScene(2);
    }

    public void TestHunter()
    {
        SceneManager.LoadScene(3);
    }

    /*public void PlaySolo()
    {
        SceneManager.LoadScene(4);
    }*/

    public void QuitGame()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }
    public void Options()
    {
        SceneManager.LoadScene("Menu Settings");
    }
}
