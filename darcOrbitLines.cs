using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darcOrbitLines : MonoBehaviour
{
    [SerializeField] float orbitLineRadius = 0;
    [SerializeField] float orbitLineWidth = 0;
    [SerializeField] LineRenderer line = null;
  
    void Awake() => DrawCircle(this.gameObject, orbitLineRadius, orbitLineWidth);

    void FixedUpdate() => this.gameObject.SetActive(false);

    public void DrawCircle(GameObject container, float radius, float lineWidth)
    {
        var segments = 360;

        if (line!=null)
        {
            line.useWorldSpace = false;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.positionCount = segments + 1;
        }

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        if (line!=null)
            line.SetPositions(points);
    }
}
