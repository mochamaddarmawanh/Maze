using UnityEngine;
using TMPro;

public class GameStopWatch : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    private bool isTimerRunning = true;

    void Update()
    {
        if (!isTimerRunning)
            return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = $"Time Running: {minutes:00}:{seconds:00}";
    }

    public void StopTimer()
    {
        isTimerRunning = false;

        Debug.Log("Final Time: " + timerText.text);
    }
}
