using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GridLayout
{
    [Header("Room Setting")]
    [SerializeField, Range(0,1)] private float tickRate;

    [SerializeField] private Transform roomContainer;
    [SerializeField] private GameObject roomPrefab;

    [SerializeField, Range(1, 10)] private int numberOfRooms;

    // Start is called before the first frame update
    void Start()
    {
        InitGrid();
        StartCoroutine(CreateRoom());
    }

    // Update is called once per frame
    void Update()
    {
        DrawDebugGrid();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateStartingRoom();
        }
    }

    /// <summary>
    ///  Main function to create the rooms for the level
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateRoom()
    {
        Vector2 roomPos = new Vector2(2, 0);  // Hard code starting pos
        int[] sequence = { 0, 0, 3, 2, 2 };   // Hard code room generation

        // Create required amount of roms
        for (int i = 0; i < numberOfRooms; i++)
        {
            // Check of the cell is empty before using it
            if (GetCellObject(roomPos).isEmpty)
            {
                CreateRoom(roomPos);
                GetCellObject(roomPos).isEmpty = false;
            }

            // Make one of the surrounding pos into a new room pos
            Vector2[] surrondingCell = GetSurroundingCell(roomPos);
            roomPos = surrondingCell[sequence[i]];

            yield return new WaitForSeconds(tickRate);
        }
    }

    /// <summary>
    /// Create a room at the given position
    /// </summary>
    /// <param name="pos"> position to create the room </param>
    private void CreateRoom(Vector2 pos)
    {
        GameObject temp = Instantiate(roomPrefab, roomContainer);
        temp.transform.position = GetGridCenter(pos);
    }

    /// <summary>
    /// Pick a random spot and make it the starting room
    /// </summary>
    private void CreateStartingRoom()
    {
        DestroyAllRooms();

        // Create Starting Room
        Vector2 startPos = GetRandomGridPos();
        CreateRoom(startPos);
    }

    /// <summary>
    /// Clear out all the generated rooms
    /// </summary>
    private void DestroyAllRooms()
    {
        foreach(Transform room in roomContainer)
        {
            Destroy(room.gameObject);
        }
    }
}
