using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomsGenerator : MonoBehaviour
{
    #region Variables
    [Range(1, 5)]
    [SerializeField] int roomsQuantityX = 4;
    [Range(1, 5)]
    [SerializeField] int roomsQuantityY = 3;

    [SerializeField] List<GameObject> roomsPrefabsList = new List<GameObject>();
    [SerializeField] List<GameObject> tmpRoomsPrefabsList = new List<GameObject>();
    [SerializeField] List<GameObject> usedRoomsList = new List<GameObject>();



    #endregion Variables

    private void Awake()
    {
        LaunchGameGeneration();
    }


    public void LaunchGameGeneration()
    {
        InitLists();
        PrepareTmpRoomsPrefabsList();
        GenerateRoomsRandomly();
    }

    void InitLists()
    {
        tmpRoomsPrefabsList.Clear();
        usedRoomsList.Clear();

    }

    void GenerateRoomsRandomly()
    {
        for (int iY = 1; iY <= roomsQuantityY; iY++)
        {
            for (int iX = 1; iX <= roomsQuantityX; iX++)
            {
                GenerateRoom();
            }
        }
    }

    void GenerateRoom()
    {
        // choose randomly a room
        int ran = UnityEngine.Random.Range(0, tmpRoomsPrefabsList.Count);
        // add it to the list of used rooms
        usedRoomsList.Add(tmpRoomsPrefabsList[ran]);
        // remove it from tmpList
        tmpRoomsPrefabsList.RemoveAt(ran);

    }

    void PrepareTmpRoomsPrefabsList()
    {
        for (int i = 0; i < roomsPrefabsList.Count; i++)
        {
            if (roomsPrefabsList[i] != null) tmpRoomsPrefabsList.Add(roomsPrefabsList[i]);
        }        
    }
}
