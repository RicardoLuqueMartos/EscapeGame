using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnableFog : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.fog = true;
            Debug.Log("Fog enabled");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.fog = false;
            Debug.Log("Fog disabled");
        }
    }
}
