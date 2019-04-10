using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("map01");
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
