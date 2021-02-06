using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcHighlightFix : MonoBehaviour
{
    Material originalMaterial = null;
    [SerializeField] Material highlightMaterial = null;
    bool isSet = false;

    void Awake() => originalMaterial = GetComponent<Renderer>().material;

    void LateUpdate()
    {
        if (darcVRPlayerController.Instance != null && darcPanelManager.Instance != null)
        {
            if (darcVRPlayerController.Instance.GetIsBeingHighlighted() && !isSet)
            {
                GetComponent<Renderer>().material = highlightMaterial;
                isSet = true;
            }
            else if (!darcVRPlayerController.Instance.GetIsBeingHighlighted() && isSet && !darcPanelManager.Instance.PanelsActive())
            {
                GetComponent<Renderer>().material = originalMaterial;
                isSet = false;
            }
        }
    }

    public void SetHighlightedMaterial() => GetComponent<Renderer>().material = highlightMaterial;
    public void SetOriginalMaterial() => GetComponent<Renderer>().material = originalMaterial;
}
