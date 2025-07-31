using UnityEngine;

public class RoomTypeManager : MonoBehaviour
{
    public static RoomTypeManager instance;

    private void Awake()
    {
        CreateInstance();
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

    public void AssignRoomType(RoomManager roomManager)
    {
        ApplyTypeToRoom(RandomizeRoomType(), roomManager);

    }

    public void ApplyTypeToRoom(RoomTypeData type, RoomManager roomManager)
    {
        RoomMaterialsManager.instance.ApplyMaterialsToSurfaces(RoomMaterialsManager.instance.RandomizeSurfacesMaterials(type), roomManager);
    }

    public RoomTypeData RandomizeRoomType()
    {
        int ran = UnityEngine.Random.Range(0, GameManager.instance.gameSettingsData.roomTypeList.Count);
        RoomTypeData response = GameManager.instance.gameSettingsData.roomTypeList[ran];
        return response;
    }

    
}
