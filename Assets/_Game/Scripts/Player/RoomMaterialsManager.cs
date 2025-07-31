using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomMaterialsManager : MonoBehaviour
{  
    public static RoomMaterialsManager instance;

    [SerializeField] List<Transform> doorsList = new List<Transform>();
    [SerializeField] List<Transform> wallsList = new List<Transform>();
    [SerializeField] List<Transform> ceilingsList = new List<Transform>();
    [SerializeField] List<Transform> floorsList = new List<Transform>();


    private void Awake()
    {
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void ResetLists()
    {
        doorsList.Clear();
        wallsList.Clear();
        ceilingsList.Clear();
        floorsList.Clear();
    }

    public void ManageRoomMaterials(RoomManager roomManager, SurfaceMaterialsData surfaceMaterialsData)
    {

    }

    public SurfaceMaterialsData RandomizeSurfacesMaterials(RoomTypeData type)
    {
        int ran = UnityEngine.Random.Range(0, type.surfaceMaterialsList.Count);
        SurfaceMaterialsData response = type.surfaceMaterialsList[ran];
        return response;
    }

    public void ApplyMaterialsToSurfaces(SurfaceMaterialsData surfacesMaterials, RoomManager roomManager)
    {
        ResetLists();

        List<Transform> tmpList = roomManager.transform.GetComponentsInChildren<Transform>().ToList();
        
        for (int i = 0; i < tmpList.Count; i++) {
            Material material = null;
            Renderer renderer = null;

            if (tmpList[i].tag == "Door")
            {
                AssignMaterial(tmpList[i].GetComponent<Renderer>(), surfacesMaterials.doorMaterial);
                doorsList.Add(tmpList[i]);
            }
            else
            {
                if (tmpList[i].name == "Cube 5v5") material = surfacesMaterials.largeSurfaceMaterial;
                else material = surfacesMaterials.smallSurfaceMaterial;

                renderer = tmpList[i].GetComponent<Renderer>();

                if (renderer != null)               
                    AssignMaterial(renderer, material);

                if (tmpList[i].tag == "Ceiling")
                    ceilingsList.Add(tmpList[i]);
                if (tmpList[i].tag == "Wall")
                    wallsList.Add(tmpList[i]);
                if (tmpList[i].tag == "Floor")
                    floorsList.Add(tmpList[i]);
            }
        }
    }

    void AssignMaterial(Renderer renderer, Material material)
    {
        if (material != null)     
            renderer.material = material;
    }
}
