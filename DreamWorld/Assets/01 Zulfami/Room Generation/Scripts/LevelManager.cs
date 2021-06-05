using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelSequenceGenerator))]
public class LevelManager : GridLayout
{
    [Header("Room Setting")]
    [SerializeField, Range(0, 1)] private float tickRate;

    [SerializeField] private Transform roomContainer;
    [SerializeField] private GameObject roomPrefab;

    [SerializeField, Range(1, 10)] private int numberOfRooms;
    private LevelSequenceGenerator levelSequence;

    private void Awake()
    {
        levelSequence = GetComponent<LevelSequenceGenerator>();
        levelSequence.numberOfRooms = numberOfRooms;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitGrid();
        StartCoroutine(NewCreateRoom());
    }

    // Update is called once per frame
    void Update()
    {
        DrawDebugGrid();
    }

    /// <summary>
    ///  Main function to create the rooms for the level
    /// </summary>
    /// <returns></returns>
    [System.Obsolete("Old Code")]
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

    private IEnumerator NewCreateRoom()
    {
        levelSequence.GenerateSequence();
        yield return new WaitForSeconds(tickRate);

        List<Vector2> roomPositions = new List<Vector2>();

        // Create First Room
        Vector2 startPos = new Vector2(gridSize / 2, gridSize / 2);
        roomPositions.Add(startPos);
        CreateRoom(startPos);

        for (int i = 0, rooms = 1; rooms < levelSequence.roomSequences.Length; i++)
        {
            // Get Room Data
            Vector2[] surroundingCell = GetSurroundingCell(startPos);
            RoomSequence sequence = levelSequence.roomSequences[i];

            // Create Room
            if (sequence.openTop && rooms <= levelSequence.roomSequences.Length)
            {
                yield return new WaitForSeconds(tickRate);
                CreateRoom(surroundingCell[0]);
                roomPositions.Add(surroundingCell[0]);
                rooms++;
            }

            if (sequence.openBottom && rooms <= levelSequence.roomSequences.Length)
            {
                yield return new WaitForSeconds(tickRate);
                CreateRoom(surroundingCell[1]);
                roomPositions.Add(surroundingCell[1]);
                rooms++;
            }

            if (sequence.openLeft && rooms <= levelSequence.roomSequences.Length)
            {
                yield return new WaitForSeconds(tickRate);
                CreateRoom(surroundingCell[2]);
                roomPositions.Add(surroundingCell[2]);
                rooms++;
            }

            if (sequence.openRight && rooms <= levelSequence.roomSequences.Length)
            {
                yield return new WaitForSeconds(tickRate);
                CreateRoom(surroundingCell[3]);
                roomPositions.Add(surroundingCell[3]);
                rooms++;
            }

            startPos = roomPositions[(i + 1) % levelSequence.roomSequences.Length];
        }
    }

    /// <summary>
    /// Create a room at the given position
    /// </summary>
    /// <param name="pos"> position to create the room </param>
    private void CreateRoom(Vector2 pos)
    {
        CellObj obj = GetCellObject(pos);
        if (!obj.isEmpty) return;
        GameObject temp = Instantiate(roomPrefab, roomContainer);
        obj.room = temp;

        temp.transform.position = GetGridCenter(pos);
        GetCellObject(pos).isEmpty = false;
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
        foreach (Transform room in roomContainer)
        {
            Destroy(room.gameObject);
        }
    }
}
