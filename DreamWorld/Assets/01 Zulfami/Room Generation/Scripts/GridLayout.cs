using UnityEngine;

public class GridLayout : MonoBehaviour
{
    public bool debug = false;
    [Header("Grid Setting")]
    [Range(1f, 20f)] public int gridSize;
    [Range(1f, 10f)] public float cellSize = 10;

    private CellObj[,] cellObjs;

    /// <summary>
    /// Init the required component of the grid
    /// </summary>
    public void InitGrid()
    {
        cellObjs = new CellObj[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                cellObjs[x, y] = new CellObj();
            }
        }
    }

    /// <summary>
    /// Draw the outline of the grid
    /// </summary>
    public void DrawDebugGrid()
    {
        if (!debug) return;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Debug.DrawLine(GridToWorld(x, y), GridToWorld(x + 1, y)); // Draw line for x axis
                Debug.DrawLine(GridToWorld(x, y), GridToWorld(x, y + 1)); // Draw line for y axis
            }
        }

        // Close the conner of the grid
        Debug.DrawLine(GridToWorld(gridSize, 0), GridToWorld(gridSize, gridSize));
        Debug.DrawLine(GridToWorld(0, gridSize), GridToWorld(gridSize, gridSize));
    }

    /// <summary>
    /// Convert the Grid index value to world space value
    /// </summary>
    /// <param name="x"> The grid X value </param>
    /// <param name="y"> The grid Y value </param>
    /// <returns> World space position of the cell </returns>
    public Vector3 GridToWorld(int x, int y)
    {
        float cellOffSet = (gridSize * cellSize / 2);                      // Have the center of the grid be at zero
        float xValue = (x * cellSize) - cellOffSet + transform.position.x; // Have the grid follow the game object
        float zValue = (y * cellSize) - cellOffSet + transform.position.z; // Have the grid follow the game object
        return new Vector3(xValue, transform.position.y, zValue);
    }

    /// <summary>
    /// Get the center of position of the cell
    /// </summary>
    /// <param name="x"> The grid X value </param>
    /// <param name="y"> The grid Y value </param>
    /// <returns> World space position of the ceneter of the cell </returns>
    public Vector3 GetGridCenter(int x, int y)
    {
        Vector3 cell = GridToWorld(x, y);
        return new Vector3(cell.x + cellSize / 2, transform.position.y, cell.z + cellSize / 2);
    }

    /// <summary>
    /// Get the center of position of the cell
    /// </summary>
    /// <param name="pos"> the position of the cell </param>
    /// <returns> World space position of the ceneter of the cell </returns>
    public Vector3 GetGridCenter(Vector2 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;
        return GetGridCenter(x, y);
    }

    /// <summary>
    /// Return the cell obj of the given cell
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public CellObj GetCellObject(Vector2 pos)
    {
        int xValue = (int)pos.x;
        int yValue = (int)pos.y;
        return cellObjs[xValue, yValue];
    }
    
    /// <summary>
    /// Get all the surrounding cell of the given cell
    /// </summary>
    /// <param name="cell"> The center cell </param>
    /// <returns> The surrounding cell </returns>
    public Vector2[] GetSurroundingCell(Vector2 cell)
    {
        Vector2[] surroundingCells = new Vector2[4];
        if (cell.y + 1 < gridSize) surroundingCells[0] = new Vector2(cell.x, cell.y + 1);
        if (cell.y - 1 >= 0) surroundingCells[1] = new Vector2(cell.x, cell.y - 1);
        if (cell.x - 1 >= 0) surroundingCells[2] = new Vector2(cell.x - 1, cell.y);
        if (cell.x + 1 < gridSize) surroundingCells[3] = new Vector2(cell.x + 1, cell.y);
        return surroundingCells;
    }

    /// <summary>
    /// Get a random position on the grid
    /// </summary>
    /// <returns> Return a random position on the grid </returns>
    public Vector2 GetRandomGridPos()
    {
        int xValue = Random.Range(0, gridSize);
        int yValue = Random.Range(0, gridSize);
        return new Vector2(xValue, yValue);
    }
}

public class CellObj
{
    public bool isEmpty = true;
    public GameObject room;
}
