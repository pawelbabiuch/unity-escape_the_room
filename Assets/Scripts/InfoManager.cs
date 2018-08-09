using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    [SerializeField]
    private Text textHeader, textDescription;
    public CanvasGroup cG;


    public void Setup(string description, string header = "Informacja")
    {
        textHeader.text = header;
        textDescription.text = description;

        if (!PanelInfoManager.ins.doWork)
            DoWork();
    }

    public void DoWork()
    {
        PanelInfoManager.ins.doWork = true;
        StartCoroutine(FadeIn());
    }

    private WaitForSeconds wfs = new WaitForSeconds(2);
    private IEnumerator FadeIn()
    {
        while(cG.alpha < 1)
        {
            cG.alpha += 0.05f;
            yield return null;
        }

        cG.alpha = 1;

        yield return wfs;

        PanelInfoManager.ins.doWork = false;

        while (cG.alpha > 0)
        {
            cG.alpha -= 0.1f;
            yield return null;
        }

        cG.alpha = 0;
    }
}
