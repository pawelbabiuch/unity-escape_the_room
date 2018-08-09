using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnlockable
{
    bool locked { get; set; }
    LockedType lockedType { get; set; }

    int keyID { get; set; }
    string passwordCode { get; set; }
    ObjectType objectType { get; set; }

    void Unlock();
}

public enum LockedType
{
    None, Key, Password, Object
}

public enum Lock01
{
    Stary, Zardzewialy, Nowy, Solidny
}

public enum Lock02
{
    niebieski, czerwony, zielony, czarny, szary, bialy
}

public enum Lock03
{
    prosty, zlozony, skomlikowany, dziwny
}
