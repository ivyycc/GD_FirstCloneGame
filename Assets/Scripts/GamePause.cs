using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePause : MonoBehaviour
{
    public GameObject pausePanel;  
    public Text displayText;       
    public Button resumeButton;    

    private bool isPaused = false;
    private float displayInterval = 10f;

    void Start()
    {
        pausePanel.SetActive(false);
        StartCoroutine(DisplayTextRoutine());
        resumeButton.onClick.AddListener(ResumeGame); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.select, this.transform.position);
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;  
        }
        else
        {
            Time.timeScale = 1f;  
        }
    }

    void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    IEnumerator DisplayTextRoutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(displayInterval);
            displayText.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            displayText.gameObject.SetActive(false);
        }
    }
}
