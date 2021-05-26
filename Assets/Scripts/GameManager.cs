using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }
}
