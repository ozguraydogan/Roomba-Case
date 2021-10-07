using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarbageController : MonoBehaviour
{
    private Vector3 target;
    private float pullingSpeed;

    private void Start()
    {
        pullingSpeed = RobotController.instance.RobotSpeed /100;
    }

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
            Debug.Log("collected");
            transform.DOMove(other.transform.position,pullingSpeed);
            target = other.transform.position;
            
        }
    }
}
