using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatCreator : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private List<Vector3> points = new List<Vector3>();
    public Camera camera;
    
    // robot Move
    [SerializeField] private float robotSpeed;
    private Vector3 target;
    private int wavepointIndex = 0;
    private bool moveStart;

    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
        }

        if (Input.GetMouseButton(0))
        {
            Draw();
        }
        if (Input.GetMouseButtonUp(0))
        {
            moveStart = true;
            target = points[0];
            
            for (int i = 0; i < points.Count; i++)
            {
                //Debug.Log("konum = " + points[i]);
            }
        }
        if (moveStart)
        {
            Move();
        }
    }

    void Draw()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo,1000))
        {
            Vector3 newPosition = hitInfo.point;
            newPosition.y = 0.4f;
            points.Add(newPosition);
            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }
    }

    void Move()
    {
        Vector3 dir = target - transform.position;
        transform.Translate(dir.normalized*robotSpeed*Time.deltaTime,Space.World );
        if (Vector3.Distance(transform.position, target) <= 0.4f)
        {
            GetNextPoint();
        }
        transform.LookAt(target);
    }
    
    void GetNextPoint()
    {
        if(wavepointIndex>=points.Count-1)
        {
            EntPoint();
            return;
        }
        wavepointIndex++;
        target = points[wavepointIndex];
    }
    void EntPoint()
    {
        moveStart = false;
       Debug.Log("Sona ulaştı");
    }
}
