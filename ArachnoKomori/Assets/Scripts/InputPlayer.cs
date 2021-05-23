using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private GameObject moveNode;

    [SerializeField] public Unit selectedUnit; //not sure if this should be public or private we'll see

    private Camera _cam;

    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        var rClick = Input.GetMouseButtonDown(1);
        var mousePos = Input.mousePosition;
        var mousePosW = _cam.ScreenToWorldPoint(mousePos);

        if (rClick) selectedUnit.OverwritePath(Instantiate(moveNode, mousePosW, Quaternion.identity));
    }
}