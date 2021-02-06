using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcHighlight : MonoBehaviour
{
    Renderer[] asteriodRenderers = null;
    Material originalMaterial = null;
    Material highlightMaterial = null;
    bool isSet = false;
    void Start()
    {
        highlightMaterial = darcVRPlayerController.Instance.GetAsteroidHighlightMaterial();
        asteriodRenderers = GetComponentsInChildren<Renderer>();
        
        if (asteriodRenderers!=null)
            originalMaterial = asteriodRenderers[0].material;
    }

    void LateUpdate() 
    {
        if (darcVRPlayerController.Instance.GetIsBeingHighlighted() && !isSet)
        {
            if (asteriodRenderers!=null)
            {
                foreach (Renderer matRenderer in asteriodRenderers)
                    matRenderer.material = highlightMaterial;
            }
            isSet = true;
        }
        else if (!darcVRPlayerController.Instance.GetIsBeingHighlighted() && isSet)
        {
            if (asteriodRenderers!=null)
            {
                foreach (Renderer matRenderer in asteriodRenderers)
                    matRenderer.material = originalMaterial;
            }
            isSet = false;
        }
    }
}
