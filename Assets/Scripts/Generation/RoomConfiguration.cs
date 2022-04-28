using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class RoomConfiguration : MonoBehaviour
{
    #region INTERNAL TYPES
    public enum Open { NULL = -1, SOUTH, NORTH, WEST, EAST }

    [System.Serializable]
    public class RoomConf
    {
        public GameObject Reference;
        public Open[] Opens;
        public GameObject[] Rooms;
    }
    #endregion

    [SerializeField] Transform _root;
    [SerializeField] RoomConf _initialRoom;
    [SerializeField] Vector2 _roomSize;

    [field: SerializeField] public List<RoomConf> RoomConfigurations { get; private set; }
    List<Room> Rooms { get; set; }


    [ContextMenu("Generate")]
    void GenerateFullMap()
    {
        if (Rooms == null) Rooms = new List<Room>();

        foreach (Transform child in _root)
        {
            DestroyImmediate(child.gameObject);
        }
        Rooms.Clear();

        //Instantiate(_roomConfigurations.PickRandom().Rooms.PickRandom(), _root);
        TryGenerateRoom(new Vector2Int(0, 0), Open.WEST, 5);
    }

    public bool TryGenerateRoom(Vector2Int coord, Open neededOpen, int currentGenerationValue)
    {
        if (neededOpen == Open.NULL) return false;
        if (currentGenerationValue < 0) return false;
        if (Rooms.FirstOrDefault(i => i.Coord == coord) != null) return false;

        var nextRoomWithDoor = CheckNextRooms(coord);
        var roomAround = new List<Open>();
        if(nextRoomWithDoor != null)
        {
            foreach (Open open in nextRoomWithDoor)
            {
                Room.FindMatch(open);
                roomAround.Add(open);
            }
        }

        var targetedRoomConf = RoomConfigurations
            .Where(i => i.Opens.Contains(neededOpen))
            .Where(i => {
                foreach(var el in roomAround)
                {
                   if(i.Opens.Contains(el)) return false;
                }
                return true;
            })
            .ToList()
            .PickRandom();

        var myNewRoom = targetedRoomConf
            .Rooms
            .PickRandom();

        var go = Instantiate(myNewRoom, new Vector3(coord.x * _roomSize.x, coord.y * _roomSize.y, 3), Quaternion.identity, _root);
        Rooms.Add(go.GetComponent<Room>());
        go.GetComponent<Room>().Generate(this, targetedRoomConf, currentGenerationValue - 1, coord);
        return true;
    }

    private List<Open> CheckNextRooms(Vector2Int coord)
    {
        var noDoorRoomAround =  new List<Open>();
        foreach(Room room in Rooms)
        {        
            if (room.Coord == new Vector2Int(coord.x + 1, coord.y))
                if (!room.roomConf.Opens.Contains<Open>(Open.WEST))
                    noDoorRoomAround.Add(Open.WEST);
            if (room.Coord == new Vector2Int(coord.x - 1, coord.y))
                if (!room.roomConf.Opens.Contains<Open>(Open.EAST))
                    noDoorRoomAround.Add(Open.EAST);
            if (room.Coord == new Vector2Int(coord.x, coord.y + 1))
                if (!room.roomConf.Opens.Contains<Open>(Open.SOUTH))
                    noDoorRoomAround.Add(Open.SOUTH);
            if (room.Coord == new Vector2Int(coord.x, coord.y - 1))
                if (!room.roomConf.Opens.Contains<Open>(Open.NORTH))
                    noDoorRoomAround.Add(Open.NORTH);      
        }

        if (noDoorRoomAround.Count == 0)
            return null;
        else
            return noDoorRoomAround;
    }
}



public static class Extension
{
    public static T PickRandom<T>(this List<T> @this)
    {
        var index = Random.Range(0, @this.Count);
        return @this[index];
    }
    public static T PickRandom<T>(this T[] @this)
    {
        var index = Random.Range(0, @this.Length);
        return @this[index];
    }

}