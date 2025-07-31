using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettingsData", menuName = "MyGame/Scriptable Objects/GameSettingsData")]
public class GameSettingsData : ScriptableObject
{
    public List<RoomTypeData> roomTypeList = new List<RoomTypeData>();
}
