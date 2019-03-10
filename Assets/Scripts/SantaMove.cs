using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaMove : MonoBehaviour
{
    public float flipSpeed = 0.25f;
    private float timeSinceFlip;

    public float moveSpeed = 0.1f;

    public Transform player;

    public SpriteRenderer sprite = null;

    public CharacterController controller = null;

    private float lastTouchTime = -10f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
        
        if (controller == null)
            controller = GetComponent<CharacterController>();
        
        timeSinceFlip = flipSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceFlip -= Time.deltaTime;

        if (timeSinceFlip <= 0)
        {
            sprite.flipX = !sprite.flipX;
            timeSinceFlip = flipSpeed;

            if (Time.fixedTime-lastTouchTime < flipSpeed)
                player.SendMessage("Hurt", 1f);
        }

        controller.SimpleMove((player.position - transform.position).normalized * moveSpeed);
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        transform.LookAt(transform.position + player.rotation * Vector3.forward,
            player.rotation * Vector3.up);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == player.gameObject.layer)
            lastTouchTime = Time.fixedTime;
    }

    void Hurt(float damage)
    {
        Destroy(gameObject);
    }
}
