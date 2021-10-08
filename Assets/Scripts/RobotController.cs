using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RobotController : MonoBehaviour
{
    public static RobotController instance;
    
    //Robot shooting distance
    [SerializeField] private float _robotSuckDistance;
    private SphereCollider _collider;

    //LineRenderer path list
    private List<Vector3> _points = new List<Vector3>();

    // robot movement speed
    [SerializeField] private float robotSpeed;
    private Vector3 _target;
    private int _wavepointIndex = 0;
    private bool _moveStart = false;
    
    public float RobotSpeed
    {
        get => robotSpeed;
    }
    
    public void SetRobotPath(List<Vector3> pts)
    {
        _points = pts; 
        _target = _points[0];
    }

    public void StartMoving()
    {
        _moveStart = true;
    }

    public void StopRobot()
    {
        _points.Clear();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        #region RobotShootingDistance

        _collider = GetComponent<SphereCollider>();
        _collider.radius = _robotSuckDistance;
        
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
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            //movement stop
            _moveStart = false;
        }
    }

    #region RobotMoving
    private void Move()
    {
        
        Vector3 dir = _target - transform.position;
        transform.Translate(dir.normalized * robotSpeed * Time.deltaTime, Space.World);
        //distance control
        if (Vector3.Distance(transform.position, _target) <= 0.4f)
        {
            GetNextPoint();
        }

        transform.LookAt(_target);
    }

    private void GetNextPoint()
    {
        if (_wavepointIndex >= _points.Count - 1)
        {
            EndPoint();
            return;
        }

        _wavepointIndex++;
        _target = _points[_wavepointIndex];
    }

    private void EndPoint()
    {
        _moveStart = false;
    }
    #endregion


    private void RunRobotParticle()
    {
        transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
    }
}