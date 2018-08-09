using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectOptionManager : MonoBehaviour
{
    public Button unlockBtn, useBtn, searchBtn, moveBtn;

    [Space]
    public Text objNameTxt;
    public Button prevBtn, nextBtn;


    public static ObjectOptionManager ins;

    private CanvasGroup canvasGroup;
    private RoomObject roomObject;

    private void Awake()
    {
        ins = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (ObjectsDetector.ins.objectsDetected.Count == 0 && canvasGroup.interactable)
            SetOffPanel();
    }

    public void ChangeObiect(int increaseID)
    {

        ObjectsDetector.ins.selectedID += increaseID;

        if (ObjectsDetector.ins.selectedID < 0)
            ObjectsDetector.ins.selectedID = ObjectsDetector.ins.objectsDetected.Count - 1;
        else if (ObjectsDetector.ins.selectedID > ObjectsDetector.ins.objectsDetected.Count - 1)
            ObjectsDetector.ins.selectedID = 0;

        SetUpPanel(ObjectsDetector.ins.selectedID);
    }

    public void Unlock()
    {
        roomObject.Unlock();
    }

    public void Use()
    {
        ((IUsable)roomObject).Use();
    }

    public void Search()
    {
        searchBtn.gameObject.SetActive(false);
        ((Storage)roomObject).Search();
    }

    public void SetUpPanel(int id)
    {
        SetOffPanel();

        roomObject = ObjectsDetector.ins.objectsDetected[id];

        if (roomObject == null) return;

        objNameTxt.text = roomObject.objectName;

        unlockBtn.gameObject.SetActive(roomObject.locked);

        if(ObjectsDetector.ins.objectsDetected.Count > 1)
        {
            nextBtn.interactable = true;
            prevBtn.interactable = true;
        }

        if (!roomObject.locked)
        {
            if (roomObject is IUsable)
                useBtn.gameObject.SetActive(true);

            if (roomObject is Storage && ((Storage)roomObject).items.Count > 0)
                searchBtn.gameObject.SetActive(true);

        }
        else if (roomObject is Door) useBtn.gameObject.SetActive(true);

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    private void SetOffPanel()
    {
        ObjectInterfaceManager.ins.SetOfInterface();

        roomObject = null;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;

        nextBtn.interactable = false;
        prevBtn.interactable = false;
        searchBtn.gameObject.SetActive(false);
        useBtn.gameObject.SetActive(false);
        searchBtn.gameObject.SetActive(false);
    }
}
