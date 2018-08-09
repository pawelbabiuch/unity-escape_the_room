using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    public List<PuzzleData> puzzleData = new List<PuzzleData>();

    public void SetUpPuzzle(int roomID, bool removePuzzleData = true)
    {
        int id = GameplayManager.random.Next(puzzleData.Count);

        Room room = GameplayManager.rooms[roomID];                                      // Pokój w którym umieszczana jest zagadka
        PuzzleData puzzle = puzzleData[id];                                             // Zagadka, która będzie przypisywana
        int clamp = Mathf.Clamp(roomID - 2, 0, roomID);

        // Lista wszystkich skrzyń z danego pokoju oraz pokoi poprzednich:
        List<Storage> storages = GameplayManager.rooms.GetRange(0, roomID + 1).Where(x => x.roomID >= clamp).SelectMany(x => x.roomObjects).Where(x => x.items.Count == 0).ToList();


        Storage previousStorage = null;                                                 // Ostatnio dodana skrzynia
        Storage storage = null;                                                         // Aktualnie rozpatrywana skrzynia
        Item firstItem = null;

        /*bool canLock = true;*/
        for (int i = 0; i < puzzle.storages.Length; i++)                                
        {
            storage = FindStorage(puzzle.storages[i], storages);                       // Wyszukuje losowej skrzynki w zanelżności od parametrów
            /* if (storage.items.Count > 0) canLock = false;*/
            storages.Remove(storage);                                                   // Usuwanie z repozytorium skrzynki, aby się nie powtórzyła w wyszukiwaniu


            for (int j = 0; j < puzzle.storages[i].itemsPrefabs.Length; j++)            // Dodawanie itemów do skrzyni
            {
                GameObject gOItem = Instantiate(puzzle.storages[i].itemsPrefabs[j], storage.transform);
                Item item = gOItem.GetComponent<Item>();
                item.SetupItem();
                storage.items.Add(item.gameObject);

          //      Debug.LogFormat("Dodano przedmiot {0} do skrzynki {1} w pokoju {2}", item.objectType, storage.name, room.roomName);

                if (j == 0) firstItem = item;
            }

            if(i == 0 /*&& canLock*/)
            {
                LockObject(room.door, firstItem);
            }
            else if(puzzle.storages[i].closePreviousObject && !previousStorage.locked /*&& canLock*/)
            {
                LockObject(previousStorage , firstItem);
            }


            previousStorage = storage;
        }

       // Debug.LogFormat("Pułapka: {0} została ustawiona.", puzzle.name);
        if (removePuzzleData) puzzleData.RemoveAt(id);
    }

    private void LockObject(RoomObject obj, Item item)
    {
        switch (item.objectType)
        {
            case ObjectType.Key:

                Key key = item.GetComponent<Key>();
                obj.SetUpObject(true, key);

                break;
            case ObjectType.Note:
                Note note = item.GetComponent<Note>();
                obj.SetUpObject( true, note);

                break;
            case ObjectType.Other:
                obj.SetUpObject(true, item.objectType);
                break;
        }
    }

    private Storage FindStorage(StoragesInfo storageInfo, List<Storage> storages)
    {
        List<Storage> available = new List<Storage>();
        int id;

        switch (storageInfo.openType)
        {
            case OpenType.Open:
                available = storages.FindAll(x => x.locked == false).Where(x => x.items.Count < x.items.Count + storageInfo.itemsPrefabs.Length).ToList();
                break;
            case OpenType.Closed:
                available = storages.FindAll(x => x.locked == true).Where(x => x.items.Count < x.items.Count + storageInfo.itemsPrefabs.Length).ToList();
                break;
            default:
                available = storages.Where(x => x.items.Count < x.items.Count + storageInfo.itemsPrefabs.Length).ToList();
                break;
        }

        id = GameplayManager.random.Next(available.Count);

        return available[id];
    }
}
