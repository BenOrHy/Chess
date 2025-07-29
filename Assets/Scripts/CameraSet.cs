using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSet : MonoBehaviour
{
    void Start()
    {
        int size = GameManager.instance.boardSize;
        float center = (size - 1) / 2f;
        float dist = -6f - (size * 0.5f);

        Camera.main.transform.position = new Vector3(center, center, dist);
    }
}
