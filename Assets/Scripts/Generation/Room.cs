using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomConfiguration;

public class Room : MonoBehaviour
{
    public Vector2Int Coord { get; private set; }

    public RoomConf roomConf;

    public static Open FindMatch(Open source)
    {
        switch(source)
        {
            case Open.EAST: return Open.WEST;
            case Open.WEST: return Open.EAST;
            case Open.NORTH: return Open.SOUTH;
            case Open.SOUTH: return Open.NORTH;
            default: return Open.NULL;
        }
    }
    public Vector2Int FindCoord(Open source)
    {
        switch (source)
        {
            case Open.EAST: return new Vector2Int(Coord.x + 1, Coord.y);
            case Open.WEST: return new Vector2Int(Coord.x - 1, Coord.y);
            case Open.NORTH: return new Vector2Int(Coord.x, Coord.y + 1);
            case Open.SOUTH: return new Vector2Int(Coord.x, Coord.y - 1);
            default: throw new Exception();
        }
    }

    internal void Generate(RoomConfiguration roomConfiguration, RoomConf room, int generationNumber, Vector2Int coord)
    {
        roomConf = room;
        if (generationNumber == 0) return;
        Coord = coord;

        foreach(var opens in room.Opens)
        {
            var matchingValue = FindMatch(opens);
            var result = roomConfiguration.TryGenerateRoom(
                coord: FindCoord(opens),
                neededOpen: matchingValue,
                currentGenerationValue: generationNumber);
        }

    }
}
