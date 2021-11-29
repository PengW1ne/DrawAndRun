using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour{

    public LineRenderer lineRenderer;
    [HideInInspector] public List<Vector2> points;
    [HideInInspector] public int pointsCount = 0;

    private float pointsMinDistance = 0.1f;
    
    public void AddPoint(Vector2 newPoint)
    {
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
        {
           return; 
        }
        points.Add(newPoint);
        pointsCount++;
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
    }

    public Vector2 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointsCount - 1);
    }
    
    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }
    
    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}
