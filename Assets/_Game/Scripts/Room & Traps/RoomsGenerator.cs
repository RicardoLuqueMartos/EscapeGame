using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoomsGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform startRoom;
    [SerializeField] Transform endRoom;
    [SerializeField] Transform generatedRooms;
    [SerializeField] Vector3 doorToDoorOffset = new();
    [SerializeField] int currentGeneratedRoomIndex = 0;
    [Range(1, 5)]
    [SerializeField] int roomsQuantityX = 4;
    [Range(1, 5)]
    [SerializeField] int roomsQuantityY = 3;
    [SerializeField] Transform _parent = null;
    [SerializeField] GameObject newroom = null;
    [SerializeField] RoomManager roomManager = null;
    [SerializeField] RoomManager parentRoomManager = null;
    [SerializeField] List<GameObject> roomsPrefabsList = new List<GameObject>();
    [SerializeField] List<GameObject> tmpRoomsPrefabsList = new List<GameObject>();
    [SerializeField] List<GameObject> usedRoomsList = new List<GameObject>();
    [SerializeField] List<Transform> generatedRoomsList = new List<Transform>();

    public static RoomsGenerator instance;

    #endregion Variables

    private void Awake()
    {
        CreateInstance();
        LaunchGameGeneration();
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        for (int iY = 1; iY <= roomsQuantityY + 1; iY++)
        {
            if (iY == roomsQuantityY + 1)
            {
                PrepareMoveEndRoom();
            }
            else
            {
                for (int iX = 1; iX <= roomsQuantityX; iX++)
                {
                    if (tmpRoomsPrefabsList.Count > 0)
                    {
                        ChooseRoom();
                        currentGeneratedRoomIndex = usedRoomsList.Count - 1;
                        GenerateRoom();
                        MoveRoom();
                        OpenDoor();                        
                    }
                }
            }
        }
    }

    void ChooseRoom()
    {
        // choose randomly a room
        int ran = UnityEngine.Random.Range(0, tmpRoomsPrefabsList.Count);
        // add it to the list of used rooms
        usedRoomsList.Add(tmpRoomsPrefabsList[ran]);
        // remove it from tmpList
        tmpRoomsPrefabsList.RemoveAt(ran);

    }

    void GenerateRoom()
    {
        newroom = Instantiate(usedRoomsList[currentGeneratedRoomIndex]);
        generatedRoomsList.Add(newroom.transform);

        if (currentGeneratedRoomIndex == 0) _parent = startRoom;
        else _parent = generatedRoomsList[generatedRoomsList.Count - 2];

        newroom.transform.parent = _parent;
        newroom.transform.localPosition = Vector3.zero;
        newroom.transform.parent = generatedRooms;
    }


    void PrepareMoveEndRoom()
    {
        newroom = endRoom.gameObject;
        _parent = generatedRoomsList[generatedRoomsList.Count - 1];

        newroom.transform.parent = _parent;
        newroom.transform.localPosition = Vector3.zero;
        newroom.transform.parent = generatedRooms;

        MoveRoom();
    }

    void MoveRoom()
    {
        roomManager = newroom.GetComponent<RoomManager>();
        parentRoomManager = _parent.GetComponent<RoomManager>();
        newroom.transform.position = new Vector3(newroom.transform.position.x+ doorToDoorOffset.x+roomManager.doorToDoorOffset.x,
            newroom.transform.position.y + doorToDoorOffset.y + roomManager.doorToDoorOffset.y, 
            newroom.transform.position.z+((roomManager.roomSizeZ*0.5f)+ parentRoomManager.roomSizeZ * 0.5f)+doorToDoorOffset.z + roomManager.doorToDoorOffset.z);
        RoomTypeManager.instance.AssignRoomType(roomManager);
    }

    void OpenDoor()
    {
        if (roomManager && roomManager.Doors.Door2.doorTransform != null)     
            roomManager.Doors.Door2.doorTransform.gameObject.SetActive(false);
        if (roomManager && roomManager.Doors.Door1.doorTransform != null)
            roomManager.Doors.Door1.doorTransform.gameObject.SetActive(false);
    }

    void PrepareTmpRoomsPrefabsList()
    {
        for (int i = 0; i < roomsPrefabsList.Count; i++)
        {
            if (roomsPrefabsList[i] != null) tmpRoomsPrefabsList.Add(roomsPrefabsList[i]);

        }        
    }
}
