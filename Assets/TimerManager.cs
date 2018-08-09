using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public int secounds = 60;
    public Text txtTimer;

    private int curSec;
    private float curValue;
    private Slider slidger;
    private DateTime dt;

    public static TimerManager ins;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        slidger = GetComponent<Slider>();
        curSec = secounds;

        int mins = curSec / 60;
        int sec = curSec % 60;
        dt = new DateTime(2017, 12, 26, 0, mins, sec, 0);

        InvokeRepeating("Tick", 0.0f, 1.0f);
    }

    private void Update()
    {
        if(slidger.value != curValue)
        {
            slidger.value = Mathf.SmoothStep(slidger.value, curValue, 1);
        }
    }

    private void Tick()
    {
        curValue = (float)curSec / secounds;
        txtTimer.text = dt.ToString("mm:ss");

        if(curSec <= 0)
        {
            CancelInvoke();
            PanelInfoManager.ins.AddInfo(InfoColor.Red, "Koniec czasu ;c");
            GameplayManager.ins.player.gameObject.SetActive(false);
            Time.timeScale = 0;
            return;
        }

        dt = dt.AddSeconds(-1);
        curSec--;
    }
}
