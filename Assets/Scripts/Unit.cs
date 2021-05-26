using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    /*
     why as custom objects rather than Vector2?
     in the future I plan on having Nodes that can move. Nodes account
     for all behaviors, such as attacking, so it is necessary for them
     to be more than Vector2 objects.
     */
    private List<GameObject> _nodePath;

    private Rigidbody2D _rbody;

    // Start is called before the first frame update
    private void Start()
    {
        _rbody = GetComponent<Rigidbody2D>();
        _nodePath = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (_nodePath.Count > 0) MoveOnPath();
    }

    public void OverwritePath(GameObject node)
    {
        for (int i = 0; i < _nodePath.Count; i++)
        {
            _nodePath[i].SetActive(false);
        }
        _nodePath = new List<GameObject> {node};
    }

    public void AppendPath(GameObject node)
    {
        _nodePath.Add(node);
    }

    public List<GameObject> GetPath()
    {
        return _nodePath;
    }

    private void MoveOnPath()
    {
        var nodePos = _nodePath[0].transform.position;
        Vector2 direction = nodePos - transform.position;
        if (direction.magnitude > 1) direction = direction.normalized;

        _rbody.velocity = direction * speed;

        if (!(direction.magnitude < 0.05)) return;
        //TODO: object pooling to save space.
        _nodePath[0].SetActive(false);
        _nodePath.Remove(_nodePath[0]);
        _rbody.velocity = Vector2.zero;
    }
    
}