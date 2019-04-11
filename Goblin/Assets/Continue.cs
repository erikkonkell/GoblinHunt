using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start game");
    }
}
