using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    [HideInInspector] public Transform[] Path;
    [HideInInspector] public float Speed;
    private float currentPathPercent;
    private Vector3[] pathPosition;
    [HideInInspector] public bool MovingIsActive;

    public void SetPath()
    {
        currentPathPercent = 0;
        pathPosition = new Vector3[Path.Length];
        for (int i = 0; i < pathPosition.Length; i++)
        {
            pathPosition[i] = Path[i].position;
        }
        transform.position = NewPositionByPath(pathPosition, 0);
        MovingIsActive = true;
    }

    private void Update()
    {
        if (MovingIsActive)
        {
            currentPathPercent += Speed / 100 * Time.deltaTime;
            transform.position = NewPositionByPath(pathPosition, currentPathPercent);
            if (currentPathPercent > 1)
            {
                GameManager.Instance.EnemyIsDestroy();
                Destroy(gameObject);
            }
        }
    }
    private Vector3 NewPositionByPath(Vector3[] pathPos, float percent)
    {
        return Interpolate(CreatePoints(pathPos), currentPathPercent);
    }

    private Vector3 Interpolate(Vector3[] path, float t)
    {
        int numSection = path.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSection), numSection - 1);
        float u = t * numSection - currPt;
        Vector3 a = path[currPt];
        Vector3 b = path[currPt + 1];
        Vector3 c = path[currPt + 2];
        Vector3 d = path[currPt + 3];
        return 0.5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
    }
    private Vector3[] CreatePoints(Vector3[] path)
    {
        Vector3[] pathPosition;
        Vector3[] newPathPos;
        int dist = 2;
        pathPosition = path;
        newPathPos = new Vector3[pathPosition.Length + dist];
        Array.Copy(pathPosition, 0, newPathPos, 1, pathPosition.Length);
        if (newPathPos.Length > 0)
        {
            newPathPos[0] = newPathPos[1] + (newPathPos[1] - newPathPos[2]);
            newPathPos[newPathPos.Length - 1] = newPathPos[newPathPos.Length - 2] + (newPathPos[newPathPos.Length - 2] - newPathPos[newPathPos.Length - 3]);
            if (newPathPos[1] == newPathPos[newPathPos.Length - 2])
            {
                Vector3[] loopSpline = new Vector3[newPathPos.Length];
                Array.Copy(newPathPos, loopSpline, newPathPos.Length);
                loopSpline[0] = loopSpline[loopSpline.Length - 3];
                loopSpline[loopSpline.Length - 1] = loopSpline[2];
                newPathPos = new Vector3[loopSpline.Length];
                Array.Copy(loopSpline, newPathPos, loopSpline.Length);
            }
        }
        return newPathPos;
    }
}
