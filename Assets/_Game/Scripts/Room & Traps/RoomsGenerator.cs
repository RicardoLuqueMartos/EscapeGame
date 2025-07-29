using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomsGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] Vector2 roomsQuantity = new Vector2(4, 3);

    [SerializeField] List<GameObject> roomsPrefabsList = new List<GameObject>();
    [SerializeField] List<GameObject> tmpRoomsPrefabsList = new List<GameObject>();



    #endregion Variables

    public void LaunchGameGeneration()
    {
        PrepareTmpRoomsPrebsList();
        GenerateRoomsRandomly();
    }

    void GenerateRoomsRandomly()
    {
        int ran = UnityEngine.Random.Range(0, tmpRoomsPrefabsList.Count);


    }

    void PrepareTmpRoomsPrebsList()
    {
        tmpRoomsPrefabsList.Clear();
        for (int i = 0; i < roomsPrefabsList.Count; i++)
        {
            if (roomsPrefabsList[i] != null) tmpRoomsPrefabsList.Add(roomsPrefabsList[i]);
        }

    }
}
