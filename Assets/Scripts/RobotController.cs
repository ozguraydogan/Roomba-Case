using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RobotController : MonoBehaviour
{
    [SerializeField] private float robotCekimMesafesi;

    private SphereCollider _collider;
    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.radius = robotCekimMesafesi;
    }
    
}
