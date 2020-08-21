using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void Restart() {
        SceneManager.LoadScene(1); //reload 1st level
    }

    public void Quit() {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
