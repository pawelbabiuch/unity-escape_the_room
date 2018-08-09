using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Item
{
    private const sbyte codeLength = 5;
    private const string codeChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string password { get; private set; }


    public string lock01, lock02;

    private static List<string> listLock01 = new List<string>() { "Zwykły", "Naderwany", "Zakurzony" };
    private static List<string> listLock02 = new List<string>() { "czerwony", "żółty", "zielony", "niebieski", "czarny", "biały" };


    public override void SetupItem()
    {
        password = "";
        objectType = ObjectType.Note;
        int id;

        for (int i = 0; i < codeLength; i++)
        {
            id = GameplayManager.random.Next(codeChars.Length);
            password += codeChars[id];
        }

        id = Random.Range(0, listLock01.Count);
        lock01 = listLock01[id];

        id = Random.Range(0, listLock02.Count);
        lock02 = listLock02[id];

        this.description = string.Format("{0} kawałek kartki w {1} kolorze. Widnieje napis: {2}", lock01, lock02, password);
       // description += string.Format("<b>{0}</b>", password);
    }
}
