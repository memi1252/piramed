using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : EnemyBase
{
    private Animator BatAnimator;

    public override void Awake()
    {
        base.Awake();
        BatAnimator = GetComponent<Animator>();
    }
    
    public override void Update()
    {
        base.Update();
        Die();
    }

    private  void Die()
    {
        if(Health <= 0)
        {
            BatAnimator.SetBool("Die", true);
            Root = false;
            StartCoroutine(DieEnemy());
        }
    }
    
    IEnumerator DieEnemy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        BatAnimator.SetTrigger("Hit");
        SpriteRenderer.color = Color.red;
        StartCoroutine(Hitfalse());
    }
    
    IEnumerator Hitfalse()
    {
        yield return new WaitForSeconds(0.5f);
        SpriteRenderer.color = Color.white;
    }
}
