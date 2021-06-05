using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSequenceGenerator : MonoBehaviour
{
    [SerializeField] private TextAsset piText;
    [SerializeField] private string seed;
    private string piValues;

    [HideInInspector] public int currentPosition;
    [HideInInspector] public int numberOfRooms;

    public RoomSequence[] roomSequences;

    private void Awake()
    {
        piValues = piText.text;
        currentPosition = SeedToInt(seed);
        roomSequences = new RoomSequence[numberOfRooms];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateSequence();
        }
    }

    public void GenerateSequence()
    {
        for (int x = 0; x < numberOfRooms; x++)
        {
            roomSequences[x].numberOfOpenRooms = (IncrementPiValue() % 3) + 1;
            for (int i = 0; i < roomSequences[x].numberOfOpenRooms;)
            {
                // Get random room pos
                int exit = IncrementPiValue() % 4;
                switch (exit)
                {
                    case 0:
                        if (roomSequences[x].openTop) break;
                        roomSequences[x].openTop = true;
                        i++;
                        break;

                    case 1:
                        if (roomSequences[x].openBottom) break;
                        roomSequences[x].openBottom = true;
                        i++;
                        break;

                    case 2:
                        if (roomSequences[x].openLeft) break;
                        roomSequences[x].openLeft = true;
                        i++;
                        break;

                    case 3:
                        if (roomSequences[x].openRight) break;
                        roomSequences[x].openRight = true;
                        i++;
                        break;
                }
            }
        }
    }

    public int IncrementPiValue()
    {
        currentPosition++;
        return GetCurrentPiValue();
    }

    public int GetCurrentPiValue()
    {
        if(currentPosition > piValues.Length)
        {
            currentPosition = 0;
        }
        return piValues[currentPosition];
    }

    private int SeedToInt(string seed)
    {
        int tempSeed = 0;
        foreach(char character in seed)
        {
            tempSeed += (int)character;
        }
        return tempSeed;
    }
}

[Serializable]
public class RoomSequence
{
    [HideInInspector] public string name = "Room";
    [Range(0,3)] public int numberOfOpenRooms = 0;
    public bool openTop, openBottom, openLeft, openRight;

    public void ResetRoom()
    {
        openTop = false;
        openBottom = false;
        openLeft = false;
        openRight = false;
    }
}
