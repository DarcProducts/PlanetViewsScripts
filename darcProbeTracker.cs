using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class darcProbeTracker : MonoBehaviour
{
    [SerializeField] float lineWidth = 0;
    [SerializeField] LineRenderer line = null;
    [SerializeField] TMP_Text probeText = null;
    GameObject startPoint;
    GameObject endPoint;
    [SerializeField] byte labelHeight = 0;
    GameObject player;

    void Start()
    {
        if (darcVRPlayerController.Instance != null)
        {
            player = darcVRPlayerController.Instance.GetPlayer();
            if (probeText != null)
                startPoint = probeText.gameObject;
            DrawLine();
        }
    }

    void OnEnable() => endPoint = FindObjectOfType<darcProbe>().gameObject;

    void FixedUpdate() => UpdateLine();

    void LateUpdate()
    {
        if (probeText != null && endPoint != null && player != null)
        {
            probeText.transform.position = new Vector3(endPoint.transform.position.x, endPoint.transform.position.y + labelHeight, endPoint.transform.position.z);
            FaceTarget(probeText);
        }
    }

    void DrawLine()
    {
        var segments = 2;
        var points = new Vector3[segments];

        if (line != null)
        {
            line.useWorldSpace = true;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.positionCount = segments;

            points[0] = startPoint.transform.position;
            points[1] = endPoint.transform.position;

            line.SetPositions(points);
        }
    }

    void UpdateLine()
    {
        var points = new Vector3[2];
        if (line != null)
        {
            points[0] = startPoint.transform.position;
            points[1] = endPoint.transform.position;

            line.SetPositions(points);
        }
    }

    void FaceTarget(TMP_Text objectToRotate) => objectToRotate.transform.rotation = Quaternion.LookRotation(objectToRotate.transform.position - player.transform.position);
}
