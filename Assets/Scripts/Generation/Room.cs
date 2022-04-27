using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomConfiguration;

public class Room : MonoBehaviour
{
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

    internal void Generate(RoomConfiguration roomConfiguration, RoomConf _initialRoom, int generationNumber)
    {
        if (generationNumber == 0) return;

        foreach(var opens in _initialRoom.Opens)
        {
            Open.
        }

    }
}
