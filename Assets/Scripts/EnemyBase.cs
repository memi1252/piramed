using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Base")]
    public int Health;
    public float RootMoveSpeed;
    public float MoveSpeed;
    
    [Header("Root")]
    public bool Root = false;
    public bool EnemyDetection = false;
    public float MaxEnemyDistance;
    public GameObject[] MoveRoots;
    private int currentWaypointIndex = 0;
    private float progress = 0f;
    
    protected SpriteRenderer SpriteRenderer;

    private bool rooot =true;

    public virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        root();
        enemydetection();
    }

    public virtual void root()
    {
        if (Root && rooot)
        {
            
            if(transform.position.x > MoveRoots[currentWaypointIndex].transform.position.x)
            {
                SpriteRenderer.flipX = true;
            }
            else
            {
                SpriteRenderer.flipX = false;
            }
            
            Transform targetWaypoint = MoveRoots[currentWaypointIndex].transform;
            progress += RootMoveSpeed * Time.deltaTime / Vector3.Distance(transform.position, targetWaypoint.position);
            transform.position = Vector3.Lerp(transform.position, targetWaypoint.position, progress);

            //Vector3 direction = MoveRoots[currentWaypointIndex].transform.position -transform.position;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
            
            if (progress >= 0.6f)
            {
                progress = 0f;
                currentWaypointIndex = (currentWaypointIndex + 1) % MoveRoots.Length;
            }
        }
    }

    private float playerAndEnemyDistance;
    
    protected virtual void enemydetection()
    {
        if (EnemyDetection && !GameManager.Instance.NotLookEnemy)
        {
            playerAndEnemyDistance =
                Vector2.Distance(transform.position, GameManager.Instance.Player.transform.position);
           

            if (MaxEnemyDistance >= playerAndEnemyDistance)
            {
                rooot = false;
                Vector3 direction = GameManager.Instance.Player.transform.position -transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-180));
                if(transform.position.x > GameManager.Instance.Player.transform.position.x)
                {
                    SpriteRenderer.flipY = false;
                    SpriteRenderer.flipX = true;
                }
                else
                {
                    SpriteRenderer.flipX = true;
                    SpriteRenderer.flipY = true;
                }
                transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.Player.transform.position, MoveSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
                SpriteRenderer.flipY = false;
                rooot = true;
            }
        }
    }
    
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
