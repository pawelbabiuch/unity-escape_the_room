using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Room inRoom { get; private set; }


    public void TeleportToRoom(Room room, bool closeRoomManager = false)
    {
        inRoom = room;

        if (closeRoomManager) RoomSelectManager.ins.Close();

        Transform spawnPoint = room.transform.Find("SpawnPoint");
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        Transform cameraViewPoint = room.transform.Find("CameraNearView");
        Camera.main.GetComponent<CameraController>().SetCurrentViewPoint(cameraViewPoint);
    }
}
