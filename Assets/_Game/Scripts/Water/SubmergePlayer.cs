using UnityEngine;
using UnityEngine.Rendering;

public class SubmergePlayer : MonoBehaviour
{

    [Header("Depth Parameters")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private int depth = 0;

    [Header("Post Processing Volume")]
    [SerializeField] private Volume postProcessingVolume;

    [Header("Post Processing Profiles")]
    [SerializeField] private VolumeProfile surfacePostProcessing;
    [SerializeField] private VolumeProfile underwaterPostProcessing;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("MainCamera"))
        {
            EnableEffects(true);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
            EnableEffects(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
            EnableEffects(false);
    }

    private void EnableEffects(bool active)
    {
        if (active)
        {
            RenderSettings.fog = true;
            postProcessingVolume.profile = underwaterPostProcessing;
        }

        else
        {
            RenderSettings.fog = false;
            postProcessingVolume.profile = surfacePostProcessing;
        }
    }
}
