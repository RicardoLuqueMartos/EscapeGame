using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomTypeData", menuName = "MyGame/Scriptable Objects/RoomTypeData")]
public class RoomTypeData : ScriptableObject
{
    public List<SurfaceMaterialsData> surfaceMaterialsList = new();
}
