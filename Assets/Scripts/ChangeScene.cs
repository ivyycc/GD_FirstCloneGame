using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.play, this.transform.position);
        SceneManager.LoadScene(sceneID);
    }
    public void StartGame()
    {
        
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
