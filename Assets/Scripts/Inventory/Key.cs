using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public int keyID { get; private set; }

    public string lock01, lock02, lock03;

    private static List<string> listLock01 = new List<string>() { "Solidny", "Nowy", "Stary", "Zardzewiały" };
    private static List<string> listLock02 = new List<string>() { "czerwony", "żółty", "zielony", "niebieski", "czarny", "biały" };
    private static List<string> listLock03 = new List<string>() { "prosty", "skomplikowany", "dziwny" };

    public override void SetupItem()
    {
        objectType = ObjectType.Key;
        keyID = GameplayManager.random.Next(0, int.MaxValue);

        int id = Random.Range(0, listLock01.Count);
        lock01 = listLock01[id];

        id = Random.Range(0, listLock02.Count);
        lock02 = listLock02[id];

        id = Random.Range(0, listLock03.Count);
        lock03 = listLock03[id];

        this.description = string.Format("{0} klucz w {1} kolorze; {2} mechanizm.", lock01, lock02, lock03);

        //      Debug.LogFormat("{0} został ustawiony na {1}", objectType, keyID);
    }
}
