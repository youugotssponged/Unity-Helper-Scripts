using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public bool isPaused;
    public GameObject PauseMenuOverlay; // To show / hide the Pause Menu UI

    private void Start()
    {
        isPaused = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
                Resume();
            else
                Pause();   
        }
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        PauseMenuOverlay.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        PauseMenuOverlay.SetActive(false);
    }

    public void ExitGameToDesktopButtonHandler()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
