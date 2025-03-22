using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Base")]
    public int Health;
    public float MoveSpeed;
    
    [Header("Root")]
    public bool Root = false;
    public GameObject[] MoveRoots;
    private int currentWaypointIndex = 0;
    private float progress = 0f;
    
    private SpriteRenderer SpriteRenderer;

    public virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        root();
    }

    public virtual void root()
    {
        if (Root)
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
            progress += MoveSpeed * Time.deltaTime / Vector3.Distance(transform.position, targetWaypoint.position);
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
    
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
