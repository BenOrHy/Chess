using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;

    public GameObject pointPrefab;
    public int boardSize = 8;
    private List<GameObject> pointPool = new List<GameObject>();

    void Awake()
    {
        instance = this;
        InitPoints();
    }

    void InitPoints()
    {
        int total = boardSize * boardSize;
        for (int i = 0; i < total; i++)
        {
            GameObject point = Instantiate(pointPrefab, Vector3.zero, Quaternion.identity, this.transform);
            point.SetActive(false);
            pointPool.Add(point);
        }
    }

    public void ShowPoints(List<Vector3> positions)
    {

        for (int i = 0; i < positions.Count && i < pointPool.Count; i++)
        {
            pointPool[i].transform.position = positions[i];
            pointPool[i].SetActive(true);
        }
    }

    public void ClearPoints()
    {
        foreach (var obj in pointPool)
        {
            obj.SetActive(false);
        }
    }
}

