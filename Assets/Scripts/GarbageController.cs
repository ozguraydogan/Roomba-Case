using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarbageController : MonoBehaviour
{
    private Vector3 _target;
    private float _pullingSpeed;

    private void Start()
    {
        _pullingSpeed = RobotController.instance.RobotSpeed /100;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target) <= 0.1f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.DOMove(other.transform.position,_pullingSpeed);
            _target = other.transform.position;
        }
    }
}

