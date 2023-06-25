using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public Transform AttackPoint;
    public LayerMask enermyLayers;
    private bool isAttacking = false;
    public float normalAttackRange;
    // Hasagi skill
    public float shootForce = 10f;
    public HasagiSkill hasagiSkill;
    public DashSkill dashSkill;
    void Start()
    {
        PlayerEntity playerEntity = GetComponent<PlayerEntity>();
        animator = gameObject.GetComponent<Animator>();
        normalAttackRange = playerEntity.AttackRange;
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
    public void NormalAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(PlayAttackAnimation());
        }
    }
    public void HasagiSkill()
    {
        hasagiSkill.ActivateHasagiSkill();
    }

    public void DashSkill()
    {
        dashSkill.dash();
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
