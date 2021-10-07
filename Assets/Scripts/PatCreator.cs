using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatCreator : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private bool _isTrue;
    private Vector3 _lastHitPos= Vector3.zero;
    private List<Vector3> points = new List<Vector3>();
    
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        #region MouseInput

        if (!_isTrue)
        {
            //The list in Robot Controller is cleared 
            if (Input.GetMouseButtonDown(0))
                RobotController.instance.StopRobot();
        
            if (Input.GetMouseButton(0))
                Drawing();
           
            if (Input.GetMouseButtonUp(0))
            {
                RobotController.instance.SetRobotPath(points);
                RobotController.instance.StartMoving(); 
                _isTrue = true;
            }
        }
        
        #endregion
    }

    void Drawing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo,100))
        {
            if ((hitInfo.point - _lastHitPos).magnitude > 0.6f)
            {
                Vector3 newPosition = hitInfo.point;
                newPosition.y = 0.4f;
                points.Add(newPosition);
                _lineRenderer.positionCount = points.Count;
                _lineRenderer.SetPositions(points.ToArray());
                _lastHitPos = newPosition;
            }
        }
    }
}
