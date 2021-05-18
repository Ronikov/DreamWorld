using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkRoom : MonoBehaviour
{
    public LinkRoom destination;
    [HideInInspector] public Transform spawnPoint;

    private void Awake()
    {
        spawnPoint = transform.GetChild(0).transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 newPos = new Vector3(destination.spawnPoint.position.x, other.transform.position.y, destination.spawnPoint.position.z);
            other.transform.position = newPos;
        }
    }
}
