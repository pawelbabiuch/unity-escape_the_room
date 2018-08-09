using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Storage : RoomObject
{
    public List<GameObject> items = new List<GameObject>();

    public void Search()
    {
        ObjectInterfaceManager.ins.SetUpInterface(ObjectsDetector.ins.selectedID);
    }
}
