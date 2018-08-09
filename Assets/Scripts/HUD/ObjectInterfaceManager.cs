using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInterfaceManager : MonoBehaviour
{
    public Text objectNameTxt;
    public Transform[] itemSlots;

    private Storage storage = null;
    private CanvasGroup canvasGroup;

    public static ObjectInterfaceManager ins;


    private void Awake()
    {
        ins = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetUpInterface(int id)
    {
        SetOfInterface();

        storage = (Storage)ObjectsDetector.ins.objectsDetected[id];

        int i = 0;
        foreach (GameObject item in storage.items)
        {
            item.transform.SetParent(itemSlots[i++]);

            RectTransform rT = item.GetComponent<RectTransform>();
            rT.localScale = Vector3.one;
            rT.offsetMin = Vector2.zero;
            rT.offsetMax = Vector2.zero;
            rT.localPosition = Vector3.zero;
        }

        storage.items.Clear();

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    public void SetOfInterface()
    {

        foreach (Transform item in itemSlots)
        {
            if (item.childCount > 0)
            {
                storage.items.Add(item.GetChild(0).gameObject);
                item.GetChild(0).SetParent(storage.transform);
            }
                
        }

        storage = null;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}
