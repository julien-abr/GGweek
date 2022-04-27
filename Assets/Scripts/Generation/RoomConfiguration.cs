using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomConfiguration : MonoBehaviour
{
    #region INTERNAL TYPES
    public enum Open { NULL = -1, SOUTH, NORTH, WEST, EAST}

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
    [field: SerializeField] public List<RoomConf> RoomConfigurations { get; private set; }


    [ContextMenu("Generate")]
    void GenerateFullMap()
    {
        foreach(Transform child in _root)
        {
            DestroyImmediate(child.gameObject);
        }

        //Instantiate(_roomConfigurations.PickRandom().Rooms.PickRandom(), _root);
        var go = Instantiate(_initialRoom.Reference, _root);
        go.GetComponent<Room>().Generate(this, _initialRoom, 20);

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