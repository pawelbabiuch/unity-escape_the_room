using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelInfoManager : MonoBehaviour
{
    public InfoManager[] infoManagers;

    public bool doWork = false;

    public static PanelInfoManager ins;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        //InvokeRepeating("CheckQueque", 1, 0.1f);
    }

    public void AddInfo(InfoColor infoColor, string descripiton, string header = "Informacja")
    {
        if (infoColor == InfoColor.Green)
            infoManagers[0].Setup(descripiton, header);
        else if (infoColor == InfoColor.Red)
            infoManagers[1].Setup(descripiton, header);
        else
            infoManagers[2].Setup(descripiton, header);
    }

}

public enum InfoColor
{
    Green, Red, Orange
}
