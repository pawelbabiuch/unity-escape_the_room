using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager ins;

    public Transform selectedSlot;
    private Transform inUseSlot;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        inUseSlot = GameObject.Find("In Use ItemSlot").transform;
    }


    public Transform inUseItem
    {
        get
        {
            if (inUseSlot.childCount == 0) return null;
            else return inUseSlot.GetChild(0);
        }
    }

    public List<ItemSlot> ItemSlots
    {
        get
        {
            return transform.GetComponentsInChildren<ItemSlot>().ToList();
        }
    }
}
