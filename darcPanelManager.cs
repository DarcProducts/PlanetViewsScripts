using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class darcPanelManager : MonoBehaviour
{
    public static darcPanelManager Instance { get; private set; }
    [SerializeField] TMP_FontAsset mainFont = null;
    [SerializeField] TMP_FontAsset statFont = null;
    [SerializeField] TMP_FontAsset labelFont = null;
    [SerializeField] GameObject panelRig;
    [SerializeField] GameObject panelRigOffset = null;
    [SerializeField] float panelRotateSpeed = 40;
    [SerializeField] int numberOfLinesPlanet = 609;
    [SerializeField] int numberOfLinesMoon = 109;
    [SerializeField] bool isPanelsActive = false;
    GameObject lastTargetHit = null;

    void Awake() => Instance = this;

    void Start()
    {
        if (panelRig != null)
            StopShowingPanels();
    }

    public void StartShowingPanels(GameObject targetHit)
    {
        lastTargetHit = targetHit;
        if (darcVRPlayerController.Instance != null)
            darcVRPlayerController.Instance.StopAllRotationAndOrbits();
        if (panelRig != null)
            panelRig.SetActive(true);
        if (darcPanelRig.Instance != null)
            darcPanelRig.Instance.StartPanels(targetHit);
        isPanelsActive = true;
        if (targetHit.GetComponent<darcHighlightFix>())
            targetHit.GetComponent<darcHighlightFix>().SetHighlightedMaterial();
    }

    public void StopShowingPanels()
    {
        //if (darcVRPlayerController.Instance != null)
            //darcVRPlayerController.Instance.StartAllRotationAndOrbits();
        if (darcPanelRig.Instance != null)
            darcPanelRig.Instance.ResetTexts();
        if (panelRig != null)
            panelRig.SetActive(false);
        isPanelsActive = false;
        if (lastTargetHit != null)
        {
            if (lastTargetHit.GetComponent<darcHighlightFix>())
                lastTargetHit.GetComponent<darcHighlightFix>().SetOriginalMaterial();
        }
    }

    public bool PanelsActive() => isPanelsActive;
    public int GetNumberOfLinesPlanet() => numberOfLinesPlanet;
    public int GetNumberOfLinesMoon() => numberOfLinesMoon;
    public TMP_FontAsset GetMainFont() => mainFont;
    public TMP_FontAsset GetLabelFont() => labelFont;
    public TMP_FontAsset GetStatFont() => statFont;
    public void RotatePanels(Vector3 rotationVector)
    {
        if (panelRigOffset != null)
            panelRigOffset.transform.Rotate(rotationVector * panelRotateSpeed * Time.deltaTime);
    }
}
