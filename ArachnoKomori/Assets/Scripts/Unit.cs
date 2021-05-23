using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    /*
     why as custom objects rather than Vector2?
     in the future I plan on having Nodes that can move. Nodes account
     for all behaviors, such as attacking, so it is necessary for them
     to be more than Vector2 objects.
     */
    private List<GameObject> nodePath;
    
    private Rigidbody2D rbody;

    [SerializeField] float speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        nodePath = new List<GameObject>();
    }
    
    void FixedUpdate()
    {
        if(nodePath.Count > 0) MoveOnPath();
    }

    public void OverwritePath(GameObject node)
    {
        nodePath = new List<GameObject>();
        nodePath.Add(node);
    }

    public void AppendPath(GameObject node)
    {
        nodePath.Add(node);
    }

    public List<GameObject> GetPath()
    {
        return nodePath;
    }

    void MoveOnPath()
    {
        var nodePos = nodePath[0].transform.position;
        Vector2 direction = (nodePos - transform.position);
        if (direction.magnitude > 1) direction = direction.normalized;

        rbody.velocity = direction * speed;

        if (direction.magnitude < 0.05)
        {
            //TODO: object pooling to save space.
            Destroy(nodePath[0]);
            nodePath.Remove(nodePath[0]);
            rbody.velocity = Vector2.zero;
        }
    }
}
