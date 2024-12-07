using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject Enemy;
    public int Count;
    public float Speed;
    public float TimeBetween;
    public Transform[] PathPoints;
    public Color PathColor = Color.yellow;

    private void Start()
    {
        StartCoroutine(CreateEnemyWave());
    }
    IEnumerator CreateEnemyWave()
    {
        for (int i = 0; i < Count; i++)
        {
            GameObject newEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
            FollowThePath followThePath = newEnemy.GetComponent<FollowThePath>();
            followThePath.Path = PathPoints;
            followThePath.Speed = Speed;
            followThePath.SetPath();
            yield return new WaitForSeconds(TimeBetween);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3[] pathposition = new Vector3[PathPoints.Length];
        for (int i = 0; i < PathPoints.Length; i++)
        {
            pathposition[i] = PathPoints[i].position;
        }
        Vector3[] newPathposition = CreatePoints(pathposition);
        Vector3 previousPosition = Interpolate(newPathposition, 0);
        Gizmos.color = PathColor;
        int smoothAmount = PathPoints.Length * 20;
        for (int i = 0; i <= smoothAmount; i++)
        {
            float t = (float)i / smoothAmount;
            Vector3 currentPosition = Interpolate(newPathposition, t);
            Gizmos.DrawLine(currentPosition, previousPosition);
            previousPosition = currentPosition;
        }
    }

    // สูตร ใช้สำหรับการจัดการ Interpolate
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
