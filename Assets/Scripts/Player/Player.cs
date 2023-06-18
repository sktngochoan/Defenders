using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();   
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        
    }
    private IEnumerator PlayAttackAnimation()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / 2); isAttacking = false;
    }
    public void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine(PlayAttackAnimation());
        }
    }
}
