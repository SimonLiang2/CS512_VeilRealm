using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // public MenuCameraController cameraController;

    public void SinglePlayer()
    {
        SceneManager.LoadSceneAsync(1); // Replace Name of the sinle player scence with whatever scrence number it will be
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }    
}
