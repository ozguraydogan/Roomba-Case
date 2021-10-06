using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarbageController : MonoBehaviour
{
    private Vector3 target;
    private void Update()
    {
        if (Vector3.Distance(transform.position, target) <= 0.1f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("toplandı");
            transform.DOMove(other.transform.position,0.2f);
            target = other.transform.position;
            
        }
    }
}
