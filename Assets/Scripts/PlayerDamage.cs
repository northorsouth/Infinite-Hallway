using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public float StartHealth = 5f;
    private float health;

    public Animator AttackAnimator = null;
    public string AttackTrigger = "Attack";
    public string AttackInput = "Attack";
    public Slider healthUI = null;
    public string AttackMessage = "Attack";
    public float Damage = 1f;
    public string LayerToAttack = "Enemy";
    public float HitRange = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        health = StartHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
        
        if (Input.GetButtonDown(AttackInput))
            AttackAnimator.SetTrigger(AttackTrigger);
    }

    void Hurt(float damage)
    {
        health -= damage;
        healthUI.value = (health/StartHealth);
    }

    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, HitRange, 1 << LayerMask.NameToLayer(LayerToAttack)))
        {
            Debug.Log("Hit!");
            hit.collider.gameObject.SendMessage(AttackMessage, Damage);
        }
    }
}
