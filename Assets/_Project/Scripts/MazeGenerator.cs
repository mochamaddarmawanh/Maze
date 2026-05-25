using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Size")]
    public int width = 15;
    public int length = 15;

    [Header("Prefabs")]
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    [Header("Settings")]
    public float wallHeight = 2f;
    public float tileSize = 4f;

    private int[,] maze;

    [Header("Player")]
    public Transform player;

    void Start()
    {
        GenerateMaze();
        BuildMaze();

        player.position = new Vector3(tileSize, 2f, tileSize);
    }

    void GenerateMaze()
    {
        maze = new int[width, length];

        // isi semua jadi wall
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                maze[x, y] = 1;
            }
        }

        Carve(1, 1);

        maze[1, 1] = 0;
        maze[1, 2] = 0;
        maze[2, 1] = 0;
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

            if (nx > 0 && ny > 0 && nx < width - 1 && ny < length - 1)
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
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);

                // floor
                Instantiate(floorPrefab, position, Quaternion.identity, transform);

                // wall
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
}