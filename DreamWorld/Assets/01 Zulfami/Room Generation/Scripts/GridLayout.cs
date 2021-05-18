using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLayout : MonoBehaviour
{
    [Range(1f, 20f)] public int gridSize;
    [Range(1f, 10f)] public float cellSize;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DrawDebugGrid();
    }

    private void DrawDebugGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Debug.DrawLine(GridToWorld(x, y), GridToWorld(x + 1, y));
                Debug.DrawLine(GridToWorld(x, y), GridToWorld(x, y + 1));
            }
        }
        Debug.DrawLine(GridToWorld(gridSize, 0), GridToWorld(gridSize, gridSize));
        Debug.DrawLine(GridToWorld(0, gridSize), GridToWorld(gridSize, gridSize));
    }

    private Vector3 GridToWorld(int x, int y)
    {
        float xValue = (x * cellSize) - (gridSize * cellSize / 2);
        float yValue = (y * cellSize) - (gridSize * cellSize / 2);
        return new Vector3(xValue, 0f, yValue);
    }
}
