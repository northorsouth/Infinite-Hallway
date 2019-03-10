using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour : StateMachineBehaviour
{
    public bool AttackOnStateEnter = false;
    public string AttackMessage = "Attack";
    public float Damage = 1f;
    public string LayerToAttack = "Enemy";
    public float HitRange = 1.5f;

    // This will be called when the animator first transitions to this state.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AttackOnStateEnter)
            Attack(animator.gameObject);
    }


    // This will be called once the animator has transitioned out of the state.
    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!AttackOnStateEnter)
            Attack(animator.gameObject);
    }

    public void Attack(GameObject attacker)
    {
        RaycastHit hit;
        if (Physics.Raycast(attacker.transform.position, attacker.transform.forward, out hit, HitRange, 1 << LayerMask.NameToLayer(LayerToAttack)))
        {
            Debug.Log("Hit!");
            hit.collider.gameObject.SendMessage(AttackMessage, Damage);
        }
    }
}
