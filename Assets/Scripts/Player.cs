using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float Movespeed = 5f;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private GameObject nife;
    [SerializeField] private float WallCheckDistance = 0.1f;
    [SerializeField] private float AttackDistance = 1f;
    [SerializeField] private float BoxOpenDistance = 0;
    [SerializeField] private LayerMask EnemyAndMos;
    [SerializeField] private LayerMask WallAndMos;

    private bool wallchekc;
    
    private Animator PlayerAnimator;
    private Animator NifeAnimator;
    public SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        PlayerAnimator = playerBody.GetComponent<Animator>();
        NifeAnimator = nife.GetComponent<Animator>();
        SpriteRenderer = playerBody.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Attack();
        BoxOpen();
    }

    private float h;
    private float v;
    
    Vector2 dir = new Vector2(0, 0);

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        dir.Set(h,v);
        
        WallCheck(dir);
        
        if (GameManager.Instance.isMove && wallchekc && GameManager.Instance.GameStart)
        {
            if (h > 0)
            {
                SpriteRenderer.flipX = false;
                NifeAnimator.SetBool("RIght", true);
            }
            else if (h < 0)
            {
                SpriteRenderer.flipX = true;
                NifeAnimator.SetBool("RIght", false);
            }

            if (dir != Vector2.zero)
            {
                PlayerAnimator.SetBool("ismove", true);
            }
            else
            {
                PlayerAnimator.SetBool("ismove", false);
            }

            transform.Translate(dir * Movespeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1") && GameManager.Instance.GameStart)
        {
            if(NifeAnimator.GetBool("Attack")) return;
            
            NifeAnimator.SetBool("Attack", true);
            if(SpriteRenderer.flipX)
            {
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.left, AttackDistance, EnemyAndMos);
                if(hit)
                {
                    if (hit.transform.gameObject.layer == 7)
                    {
                        hit.transform.GetComponent<EnemyBase>().TakeDamage(1);
                    }
                    if (hit.transform.gameObject.layer == 8)
                    {
                        Destroy(hit.transform.gameObject);
                    }
                    
                    
                }
            }
            else
            {
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.right, AttackDistance, EnemyAndMos);
                if(hit)
                {
                    if (hit.transform.gameObject.layer == 7)
                    {
                        hit.transform.GetComponent<EnemyBase>().TakeDamage(1);
                    }
                    if (hit.transform.gameObject.layer == 8)
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
        else
        {
            NifeAnimator.SetBool("Attack", false);
        }
    }

    private bool hitRight;
    private bool hitLeft;
    private bool hitUp;
    private bool hitDown;
    private void WallCheck(Vector2 dir)
    {
        hitRight = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.right, WallCheckDistance, WallAndMos);
        hitLeft = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.left, WallCheckDistance, WallAndMos);
        hitUp = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.up, WallCheckDistance, WallAndMos);
        hitDown = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, Vector2.down, WallCheckDistance, WallAndMos);

        if ((hitRight && dir.x > 0) || (hitLeft && dir.x < 0) || (hitUp && dir.y > 0) || (hitDown && dir.y < 0))
        {
            wallchekc = false;
        }
        else
        {
            wallchekc = true;
        }
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.right *WallCheckDistance, new Vector3(0.5f, 0.5f, 0));
        Gizmos.DrawWireCube(transform.position + Vector3.left *WallCheckDistance, new Vector3(0.5f, 0.5f, 0));
        Gizmos.DrawWireCube(transform.position + Vector3.up * WallCheckDistance, new Vector3(0.5f, 0.5f, 0));
        Gizmos.DrawWireCube(transform.position + Vector3.down * WallCheckDistance, new Vector3(0.5f, 0.5f, 0));
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.right * AttackDistance, new Vector3(0.5f, 0.5f, 0));
        Gizmos.DrawWireCube(transform.position + Vector3.left * AttackDistance, new Vector3(0.5f, 0.5f, 0));
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BoxOpenDistance);
        #endif
    }
    
    public void TakeDamage(int damage)
    {
        GameManager.Instance.PlayerHealth -= damage;
    }

    private void BoxOpen()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Box box in GameManager.Instance.Boxs)
            {
                float distance = Vector2.Distance(transform.position, box.transform.position);
                if (distance < BoxOpenDistance)
                {
                    if (GameManager.Instance.boxWhere)
                    {
                        GameManager.Instance.boxWhere = false;
                    }
                    Animator boxAnim = box.GetComponent<Animator>();
                    boxAnim.SetBool("BoxOpen", true);
                    Box boxScript = box.GetComponent<Box>();
                    boxScript.OpenBox();
                    
                }
            }
        }
    }
}
