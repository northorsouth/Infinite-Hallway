using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public float health = 5f;

    public Animator AttackAnimator = null;
    public string AttackTrigger = "Attack";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        RaycastHit hit;
        if (Input.GetButtonDown("Attack (XBox)") || Input.GetButtonDown("Attack (Keyboard)"))
            AttackAnimator.SetTrigger(AttackTrigger);
    }

    void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Damaged: " + damage);
    }
}
