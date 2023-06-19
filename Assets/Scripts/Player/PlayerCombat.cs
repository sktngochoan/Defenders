using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;

    public Transform AttackPoint;
    public float normalAttackRange = 0.5f;
    public LayerMask enermyLayers;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
    }
    private IEnumerator PlayAttackAnimation()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        Collider2D[] hitEnermy =  Physics2D.OverlapCircleAll(AttackPoint.position, normalAttackRange, enermyLayers);
        foreach (Collider2D item in hitEnermy)
        {
            Debug.Log("A a a" + item.name);
        }
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / 2); isAttacking = false;
    }
    public void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine(PlayAttackAnimation());
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, normalAttackRange);
    }
}
