using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoManager : MonoBehaviour
{
    public static ItemInfoManager ins;

    [SerializeField]
    private Text itemName, itemDescription;
    private CanvasGroup cG;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        cG = GetComponent<CanvasGroup>();
    }

    public void Setup(string itemName, string itemDescription)
    {
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;

        cG.alpha = 1;
        cG.blocksRaycasts = true;
    }

    public void Close()
    {
        cG.alpha = 0;
        cG.blocksRaycasts = false;
    }
}
