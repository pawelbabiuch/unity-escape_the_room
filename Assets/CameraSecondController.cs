using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSecondController : MonoBehaviour
{
    public Transform farView;
    public Transform[] nearViews;

    private bool isLookingFar;
    [SerializeField]
    private int selectedNearView = 0;
    private Vector3 currentView;
    private void Start()
    {
        currentView = nearViews[selectedNearView].position;
        isLookingFar = false;
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            selectedNearView = Mathf.Clamp(selectedNearView - 1, 0, nearViews.Length - 1);
            currentView = nearViews[selectedNearView].position;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            selectedNearView = Mathf.Clamp(selectedNearView + 1, 0, nearViews.Length - 1);
            currentView = nearViews[selectedNearView].position;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLookingFar = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isLookingFar = false;
        }

        if(!isLookingFar)
        {
            transform.position = Vector3.Lerp(transform.position, currentView, Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, farView.position, Time.deltaTime);
        }
    }
}
