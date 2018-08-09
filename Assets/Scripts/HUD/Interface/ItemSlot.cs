using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        InterfaceManager.ins.selectedSlot = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InterfaceManager.ins.selectedSlot = null;
    }
}

