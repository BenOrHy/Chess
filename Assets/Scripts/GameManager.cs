using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool isDevelopmentMode = false;
    public bool isGaming = true;
    public bool inMenu = true;

    public int boardSize = 8;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
