using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RoomSelectManager : MonoBehaviour
{
    public GameObject[] roomsBtnGO;

    public static RoomSelectManager ins;
    private CanvasGroup cG;

    private bool setup = false;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        cG = GetComponent<CanvasGroup>();
    }

    public void Setup(Room[] rooms)
    {
        if(!setup)
        {
            for (int i = 0; i < roomsBtnGO.Length; i++)
                roomsBtnGO[i].GetComponentInChildren<Text>().text = GameplayManager.rooms[i].roomName;

            setup = true;
        }

        GameplayManager.ins.player.GetComponent<PlayerController>().enabled = false;
        List<GameObject> btns = roomsBtnGO.Where(x=> rooms.Any(y=>y.roomID == x.transform.GetSiblingIndex())).ToList();

        foreach (GameObject btn in roomsBtnGO) btn.SetActive(false);
        foreach (GameObject btn in btns) btn.SetActive(true);

        cG.alpha = 1;
        cG.blocksRaycasts = true;
    }

    public void Close()
    {
        cG.alpha = 0;
        cG.blocksRaycasts = false;
        GameplayManager.ins.player.GetComponent<PlayerController>().enabled = true;
    }

    public void Teleport(int roomID)
    {
        Room r = GameplayManager.rooms.Find(x=>x.roomID == roomID);
        GameplayManager.ins.player.GetComponent<PlayerManager>().TeleportToRoom(r, true);
    }
}
