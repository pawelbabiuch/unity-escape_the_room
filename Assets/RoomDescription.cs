using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomDescription : MonoBehaviour
{
    [SerializeField]
    private Text itemNameTxt, itemDescriptionTxt;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private bool isOn = false;

    public static RoomDescription ins;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        Disable();
    }

    private void Update()
    {
        if (isOn)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void SetUp(string itemName, string itemDescription)
    {
        itemNameTxt.text = itemName;
        itemDescriptionTxt.text = itemDescription;
        canvasGroup.alpha = 1;
        isOn = true;
    }

    public void Disable()
    {
        canvasGroup.alpha = 0;
        isOn = false;
    }
}
