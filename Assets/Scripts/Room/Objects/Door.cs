using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : RoomObject, IUsable
{
    public Room roomUnlock;

    private void Start()
    {
        locked = true;
    }

    public void Use()
    {
        if (roomUnlock == null && !locked)
        {
            PanelInfoManager.ins.AddInfo(InfoColor.Green, "Koniec gry");
            TimerManager.ins.CancelInvoke();
            Time.timeScale = 0;
            //Debug.LogError("Koniec gry!");
        }
        else
        {
            Room[] rooms = GameplayManager.rooms.Where(x => x.Available == true && GameplayManager.ins.player.inRoom != x).ToArray();
            RoomSelectManager.ins.Setup(rooms);
        }
    }
}
