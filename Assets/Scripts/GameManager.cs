using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartCubeLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void StartBallLevel()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        #if UNITY_STANDALONE
            //Quit the application
            Application.Quit();
        #endif
        
                //If we are running in the editor
        #if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    
}
