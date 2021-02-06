using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcPositionLine : MonoBehaviour
{
    [SerializeField] float lineWidth = 0;
    [SerializeField] LineRenderer line = null;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
  
    void Start() => DrawLine();
    
    void FixedUpdate()
    {
        UpdateLine();
        this.gameObject.SetActive(false);
    }

    void DrawLine()
    {
        var segments = 2;

        if (line!=null)
        {
            line.useWorldSpace = true;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.positionCount = segments;
        }

        var points = new Vector3[segments];
        if (points!=null)
        {
            points[0] = startPoint.transform.position;
            points[1] = endPoint.transform.position;
        }

        if (line!=null)
            line.SetPositions(points);
    }

    void UpdateLine()
    {   
        var points = new Vector3[2];
        if (points!=null)
        {
            points[0] = startPoint.transform.position;
            points[1] = endPoint.transform.position;
        }

        if (line!=null)
            line.SetPositions(points);
    }
}
