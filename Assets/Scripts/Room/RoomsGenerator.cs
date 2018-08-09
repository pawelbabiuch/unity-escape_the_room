using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(GameplayManager))]
[RequireComponent(typeof(PuzzleManager))]
public class RoomsGenerator : MonoBehaviour
{
    public int roomCount = 7;
    public GameObject[] roomsPrefabs;
    public Transform[] spawnPoints;

    public static RoomsGenerator ins;

    private void Awake()
    {
        ins = this;
    }

    public void Setup()
    {
        if (roomsPrefabs.Length < roomCount || spawnPoints.Length < roomCount)
        {
            Debug.LogError("Zbyt mała ilość pokoi lub punktów spawnu!");
            return;
        }

        Mix(roomsPrefabs);
        Mix(spawnPoints);
        InstantiateRooms();

        PuzzleManager pM = GetComponent<PuzzleManager>();
        for (int i = 0; i < roomCount; i++) pM.SetUpPuzzle(i, false);      // Ustawianie pułapek
    }


    /// <summary>
    /// Wstawianie pokoi oraz ustawianie gracza
    /// </summary>
    private void InstantiateRooms()
    {
        Room lastRoom = null;
        for (int i = 0; i < roomCount; i++)
        {
            Room room = Instantiate(roomsPrefabs[i], spawnPoints[i].position, Quaternion.identity, this.transform).GetComponent<Room>();
            room.roomObjects = room.transform.Find("Objects").GetComponentsInChildren<Storage>();
            room.door = room.transform.GetComponentInChildren<Door>();
            GameplayManager.rooms.Add(room);

            if (lastRoom != null)
                lastRoom.door.roomUnlock = room;


            lastRoom = room;
            Destroy(spawnPoints[i].gameObject);

        }

        GameplayManager.ins.player.TeleportToRoom(transform.GetChild(roomCount).GetComponent<Room>());
        transform.GetChild(roomCount).GetComponent<Room>().Available = true;
    }


    /// <summary>
    /// Mieszanie tablicy obiektów
    /// </summary>
    /// <param name="objects">Tablica obiektów</param>
    private void Mix(object[] objects)
    {
        List<object> list = new List<object>(objects.Length);
        list = objects.ToList();

        for (int i = 0; i < objects.Length; i++)
        {
            int id = GameplayManager.random.Next(list.Count);
            objects[i] = list[id];

            list.RemoveAt(id);
        }
    }
}
