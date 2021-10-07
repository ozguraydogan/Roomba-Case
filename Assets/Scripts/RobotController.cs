using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RobotController : MonoBehaviour
{
    public static RobotController instance;
    
    public void SetRobotPath(List<Vector3> _points)
    {
        points = _points;
        target = _points[0];
    }

    public void StartMoving()
    {
        _moveStart = true;
    }

    public void StopRobot()
    {
        points.Clear();
    }

    public float RobotSpeed
    {
        get { return robotSpeed; }
    }


    //Robot shooting distance
    [SerializeField] private float robotCekimMesafesi;
    private SphereCollider _collider;

    //LineRenderer path list
    List<Vector3> points = new List<Vector3>();

    // robot Move
    [SerializeField] private float robotSpeed;
    private Vector3 target;
    private int _wavepointIndex = 0;
    private bool _moveStart = false;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        #region RobotShootingDistance

        _collider = GetComponent<SphereCollider>();
        _collider.radius = robotCekimMesafesi;

        #endregion
    }

    private void Update()
    {
        if (_moveStart)
        {
            RunRobotParticle();
            Move();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            //If it hits an obstacle, the explosion effect will be active and robot stop 
            this.transform.GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(false);
            
            _moveStart = false;
        }
    }

    private void Move()
    {
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized * robotSpeed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target) <= 0.4f)
        {
            GetNextPoint();
        }

        transform.LookAt(target);
    }

    private void GetNextPoint()
    {
        if (_wavepointIndex >= points.Count - 1)
        {
            EndPoint();
            return;
        }

        _wavepointIndex++;
        target = points[_wavepointIndex];
    }

    private void EndPoint()
    {
        _moveStart = false;
    }

    private void RunRobotParticle()
    {
        transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
    }
}