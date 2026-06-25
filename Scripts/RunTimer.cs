using UnityEngine;
using UnityEngine.UI;

public class RunTimer : MonoBehaviour
{
    public Text timerText;
    public KeyCode restartKey = KeyCode.R;

    private float startTime;
    private bool running = false;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (running)
        {
            float t = Time.time - startTime;
            UpdateDisplay(t);
        }

        if (Input.GetKeyDown(restartKey))
        {
            RestartRun();
        }
    }

    public void StartRun()
    {
        startTime = Time.time;
        running = true;
    }

    public void StopRun()
    {
        running = false;
    }

    public void ResetTimer()
    {
        startTime = Time.time;
        running = false;
        UpdateDisplay(0f);
    }

    void UpdateDisplay(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60f);
        int seconds = Mathf.FloorToInt(t % 60f);
        int millis = Mathf.FloorToInt((t * 1000f) % 1000f);
        timerText.text = $"{minutes:00}:{seconds:00}.{millis:000}";
    }

    void RestartRun()
    {
        // Simple version: reload scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
