using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        transform.Rotate(0, Input.GetAxis("Mouse X") * 1f, 0);
    }
}
