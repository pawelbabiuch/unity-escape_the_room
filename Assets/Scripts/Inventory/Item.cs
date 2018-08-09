using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public string itemName;
    [DisableWhenRun]
    public ObjectType objectType;
    [DisableWhenRun]
    public string description;

    private Image image;
    private static Transform lastSlot;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public virtual void SetupItem()
    {
        objectType = ObjectType.Other;
       // Debug.LogFormat("{0} został ustawiony", objectType);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        lastSlot = transform.parent;
        transform.SetParent(InterfaceManager.ins.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        if (InterfaceManager.ins.selectedSlot && InterfaceManager.ins.selectedSlot.childCount == 0)
            transform.SetParent(InterfaceManager.ins.selectedSlot);
        else
            transform.SetParent(lastSlot);

        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
        {
            ItemSlot itemSlot = InterfaceManager.ins.ItemSlots.FirstOrDefault(x=>x.transform.childCount == 0);

            if(itemSlot != null)
            {
                transform.SetParent(itemSlot.transform);
                GetComponent<RectTransform>().offsetMin = Vector2.zero;
                GetComponent<RectTransform>().offsetMax = Vector2.zero;
            }
        }
        else if(eventData.clickCount == 1)
        {
            ItemInfoManager.ins.Setup(itemName, description);
        }
    }
}

public enum ObjectType
{
    Key, Note, Other
}