using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{

    [SerializeField] private GameObject moveNode;
    
    [SerializeField] public Unit selectedUnit; //not sure if this should be public or private we'll see

    private Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        bool rClick = Input.GetMouseButtonDown(1);
        var mousePos = Input.mousePosition;
        var mousePosW = cam.ScreenToWorldPoint(mousePos);

        if (rClick)
        {
            selectedUnit.OverwritePath(Instantiate(moveNode, mousePosW, Quaternion.identity));
        }
    }
}
