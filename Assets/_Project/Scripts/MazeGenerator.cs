using UnityEngine;
using StarterAssets;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Size")]
    [SerializeField] private int mazeWidth = 15;
    [SerializeField] private int mazeHeight = 15;
    private int[,] maze;

    [Header("Settings")]
    [SerializeField] private float wallHeight = 2f;
    [SerializeField] private float tileSize = 4f;

    [Header("Prefabs")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject exitPrefab;

    [Header("Player")]
    private Transform player;
    private CharacterController characterController;
    private ThirdPersonController thirdPersonController;

    void Start()
    {
        GenerateMaze();

        BuildMaze();

        SpawnExit();

        player =
            GameObject
            .FindGameObjectWithTag("Player")
            .transform;

        characterController =
            player.GetComponentInChildren<CharacterController>();

        thirdPersonController =
            player.GetComponentInChildren<ThirdPersonController>();

        Invoke(nameof(SpawnPlayer), 0.1f);
    }

    void SpawnPlayer()
    {
        thirdPersonController.enabled = false;
        characterController.enabled = false;

        player.position = new Vector3(
            tileSize,
            2f,
            tileSize
        );

        characterController.enabled = true;

        Invoke(nameof(EnablePlayerController), 0.2f);
    }

    void EnablePlayerController()
    {
        thirdPersonController.enabled = true;
    }

    void GenerateMaze()
    {
        maze = new int[mazeWidth, mazeHeight];

        // isi semua jadi wall
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = 1;
            }
        }

        Carve(1, 1);
    }

    void Carve(int x, int y)
    {
        int[] directions = { 0, 1, 2, 3 };
        Shuffle(directions);

        maze[x, y] = 0;

        foreach (int dir in directions)
        {
            int dx = 0;
            int dy = 0;

            switch (dir)
            {
                case 0: dy = 2; break; // up
                case 1: dy = -2; break; // down
                case 2: dx = 2; break; // right
                case 3: dx = -2; break; // left
            }

            int nx = x + dx;
            int ny = y + dy;

            if (nx > 0 && ny > 0 && nx < mazeWidth - 1 && ny < mazeHeight - 1)
            {
                if (maze[nx, ny] == 1)
                {
                    maze[x + dx / 2, y + dy / 2] = 0;
                    Carve(nx, ny);
                }
            }
        }
    }

    void Shuffle(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int random = Random.Range(i, array.Length);

            int temp = array[i];
            array[i] = array[random];
            array[random] = temp;
        }
    }

    void BuildMaze()
    {
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);

                Instantiate(floorPrefab, position, Quaternion.identity, transform);

                if (maze[x, y] == 1)
                {
                    Vector3 wallPos = position + Vector3.up * (wallHeight / 2f);

                    GameObject wall = Instantiate(
                        wallPrefab,
                        wallPos,
                        Quaternion.identity,
                        transform
                    );

                    wall.transform.localScale = new Vector3(
                        tileSize,
                        wallHeight,
                        tileSize
                    );
                }
            }
        }
    }

    void SpawnExit()
    {
        Vector3 exitPosition = Vector3.zero;

        for (int x = mazeWidth - 2; x >= 0; x--)
        {
            for (int y = mazeHeight - 2; y >= 0; y--)
            {
                if (maze[x, y] == 0)
                {
                    exitPosition = new Vector3(
                        x * tileSize,
                        1f,
                        y * tileSize
                    );

                    Instantiate(
                        exitPrefab,
                        exitPosition,
                        Quaternion.identity
                    );

                    return;
                }
            }
        }
    }
}