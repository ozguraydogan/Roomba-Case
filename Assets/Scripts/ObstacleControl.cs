using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControl : MonoBehaviour
{
    [SerializeField] private GameObject robotExplosion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            robotExplosion.SetActive(true);
            Debug.Log("engele çarptı");
        }
    }
}
