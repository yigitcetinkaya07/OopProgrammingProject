using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    //By creating a focalPoint in Unity editor, we made the camera its child object. We rotated the camera by rotating the focalpoint.
    [SerializeField]
    private float rotationSpeed;
    private float horizontalInput;
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") * -1;
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

}
