using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public GameStopWatch gameStopWatch;

    void Start()
    {
        gameStopWatch = FindAnyObjectByType<GameStopWatch>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameStopWatch.StopTimer();

            Debug.Log("YOU WIN!");
        }
    }
}