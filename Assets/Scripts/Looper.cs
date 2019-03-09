using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour
{
    public enum loopAxis { x, y, z };
    public loopAxis LoopAxis = loopAxis.z;
    
    public float maxDistance = 25;

    private float startPos;

    private CharacterController character;
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        startPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.z) - startPos >= maxDistance)
        {
            Vector3 tempPos = transform.position;
            tempPos.z = startPos;
            character.enabled = false;
            transform.position = tempPos;
            character.enabled = true;
        }
    }
}
