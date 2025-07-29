using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public bool isDragging;
    private Vector3 offset;
    public Vector3 mainPos;
    public float snapRange = 0.5f;
    public List<Vector3> snapPoints = new List<Vector3>();

    public Vector3 initiateLoc;

    public absUnit unit;
    public PointManager points;

    float objectZ;

    void Start()
    {
        unit = this.GetComponent<absUnit>();

        points = PointManager.instance;

        initiateLoc = this.transform.position;

        if (unit.Points != null && unit.Points.Count > 0)
        {
            snapPoints = unit.Points;
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    snapPoints.Add(new Vector3(i, j, 0));
                }
            }
        }
    }

    void OnMouseDown()
    {
        if (!isDragging)
        {
            points.ShowPoints(unit.Points);
            isDragging = true;
        }

        objectZ = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = objectZ;
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);

        offset = transform.position - worldMouse;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = objectZ;
            Vector3 worldMouse = Camera.main.ScreenToWorldPoint(mousePos);

            transform.position = worldMouse + offset;
        }
    }

    void OnMouseUp()
    {
        points.ClearPoints();
        isDragging = false;

        Vector3 closest = GetClosestSnapPointInRange();
        transform.position = closest;
        mainPos = closest;

        if (initiateLoc != closest)
        {
            unit.initiate = false;
        }

        unit.mainPos = closest;
        initiateLoc = closest;
        unit.position();
    }

    Vector3 GetClosestSnapPointInRange()
    {
        Vector3 closest = initiateLoc;
        float minDist = snapRange;
        Vector3 currentPos = transform.position;

        foreach (Vector3 point in snapPoints)
        {
            float dist = Vector3.Distance(currentPos, point);
            if (dist <= minDist)
            {
                minDist = dist;
                closest = point;
            }
        }

        return closest;
    }
}
