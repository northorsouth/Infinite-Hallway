using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycler : MonoBehaviour
{
    public float CellSize = 5;
    public Transform target;

    private Transform first;
    private Transform last;
    
    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount <= 1)
            Debug.Log("What the fuck dude");
        
        first = transform.GetChild(0);
        last = transform.GetChild(transform.childCount-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (((last.position - target.position).magnitude - (first.position - target.position).magnitude) >= CellSize)
        {
            last.SetAsFirstSibling();
            last.position = first.position + ((first.position - last.position).normalized * CellSize);

            first = last;
            last = transform.GetChild(transform.childCount-1);
            Debug.Log("Closer to first");
        }

        if (((first.position - target.position).magnitude - (last.position - target.position).magnitude) >= CellSize)
        {
            first.SetAsLastSibling();
            first.position = last.position + ((last.position - first.position).normalized * CellSize);

            last = first;
            first = transform.GetChild(0);
            Debug.Log("Closer to last");
        }
    }
}
