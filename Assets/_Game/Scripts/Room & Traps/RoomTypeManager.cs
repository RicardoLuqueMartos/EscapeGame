using UnityEngine;

public class RoomTypeManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] RoomMaterialsManager roomMaterialsManager;
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
        roomMaterialsManager.ApplyMaterialsToSurfaces(roomMaterialsManager.RandomizeSurfacesMaterials(type), roomManager);
    }

    public RoomTypeData RandomizeRoomType()
    {        
        int ran = UnityEngine.Random.Range(0, gameManager.gameSettingsData.roomTypeList.Count);
        RoomTypeData response = gameManager.gameSettingsData.roomTypeList[ran];
        return response;
    }

    
}
