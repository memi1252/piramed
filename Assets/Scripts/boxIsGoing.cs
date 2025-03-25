using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxIsGoing : MonoBehaviour
{
    [SerializeField] public GameObject[] Doors;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("MoveBox"))
        {
            for(int i = 0; i<Doors.Length; i++)
            {
                Destroy(Doors[i]);
            }
            Debug.Log("openDoor");
        }
    }
}
