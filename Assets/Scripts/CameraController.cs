using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int cameraSpeed = 5;

    private Transform currentViewPoint;
    
    [SerializeField]
    private Transform farViewPoint;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            transform.position = Vector3.Lerp(transform.position, farViewPoint.position, cameraSpeed * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, currentViewPoint.position, cameraSpeed * Time.deltaTime);
    }


    public void SetCurrentViewPoint(Transform viewPoint)
    {
        currentViewPoint = viewPoint;
    }
}
