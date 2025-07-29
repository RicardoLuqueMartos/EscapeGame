using System;
using UnityEngine;


public class RoomManager : MonoBehaviour
{
    #region Variables
    [Serializable]
    public class DoorData
    {
        public Transform doorTransform;
        public int onWallPositionX = 0;
        public int onWallPositionY = 0;
    }


    [Serializable]
    public class DoorsData
    {
        public DoorData Door1 = new DoorData();
        public DoorData Door2 = new DoorData();
        public DoorData Door3 = new DoorData();
        public DoorData Door4 = new DoorData();
        public DoorData Door5 = new DoorData();
        public DoorData Door6 = new DoorData();
    }
    public DoorsData Doors = new DoorsData();

    #endregion Variables


}
