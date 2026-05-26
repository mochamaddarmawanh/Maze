using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    private GameStopWatch gameStopWatch;

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

            Invoke(nameof(RestartGame), 2f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}