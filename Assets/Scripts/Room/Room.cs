using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Room : MonoBehaviour
{
    [DisableWhenRun]
    public string roomName = "";
    private bool available = false;

   // [HideInInspector]
    public Storage[] roomObjects;
  //  [HideInInspector]
    public Door door;

    public bool Available
    {
        get { return available; }
        set
        {
            transform.Find("FogOfWar").gameObject.SetActive(!value);
            available = value;
        }
    }

    public int roomID
    {
        get
        {
            return transform.GetSiblingIndex();
        }
    }
}
