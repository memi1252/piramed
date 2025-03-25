using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private bool isDamage = false;
    [SerializeField] private int damage = 10;
    public bool timer = false;
    public float iiii = 6;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (!isDamage && !timer)
            {
                animator.SetBool("isPush", true);
                other.GetComponent<Player>().SpriteRenderer.color = Color.red;
                StartCoroutine(DamegeColor(other.GetComponent<Player>().SpriteRenderer));
                StartCoroutine(DamageFalse());
                isDamage = true;
                timer = true;
            }else if (isDamage && timer)
            {
                Player player = other.GetComponent<Player>();
                player.TakeDamage(damage);
                isDamage = false;
            }
            
        }
    }

    private void Update()
    {
        if(timer)
        {
            iiii -= Time.deltaTime;
            if (iiii <= 0)
            {
                timer = false;
                isDamage = false;
                iiii = 6;
            }
        }
    }
    
    IEnumerator DamageFalse()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isPush", false);
    }

    IEnumerator DamegeColor(SpriteRenderer spriteRenderer)
    {
        yield return new WaitForSeconds(0.4f);
        spriteRenderer.color = Color.white;
    }
}
