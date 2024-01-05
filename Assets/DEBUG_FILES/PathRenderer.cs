using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathRenderer : MonoBehaviour
{
    LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawPathUnderGravity(Vector3 startPosition, Vector3 velocity, float gravity, float groundLevel, float resolutionTime)
    {
        List<Vector3> pointsOnPath = new List<Vector3>();

        bool groundLevelReached = false;
        int maxiterations = 1000;
        int currentIteration = 0;

        Vector3 newPointOnPath = startPosition;
        while (!groundLevelReached && currentIteration < maxiterations) 
        {
            pointsOnPath.Add(newPointOnPath);
            velocity += new Vector3(0, -gravity,0) * resolutionTime;
            newPointOnPath += velocity * resolutionTime;
            if(newPointOnPath.y <= 0)
            {
                groundLevelReached = true;
            }

            currentIteration++;
        }

        lineRenderer.positionCount = pointsOnPath.Count;
        lineRenderer.SetPositions(pointsOnPath.ToArray());
    }
}
