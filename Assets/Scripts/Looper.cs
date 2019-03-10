using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour
{
    public enum loopAxis { x, y, z };
    public loopAxis LoopAxis = loopAxis.z;
    
    public float maxDistance = 25;

    private float startPos;

    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.transform.position.z);
        if (Mathf.Abs(player.transform.position.z - startPos) >= maxDistance)
        {
            float delta = startPos - player.transform.position.z;
            foreach (CharacterController character in transform.GetComponentsInChildren<CharacterController>())
            {
                character.enabled = false;
                Vector3 tempPos = character.transform.position;
                tempPos.z = tempPos.z + delta;
                character.transform.position = tempPos;
                character.enabled = true;
            }
        }
    }
}
