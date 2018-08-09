using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    [DisableWhenRun]
    [Tooltip("Ziarno losowania poziomu, gdy puste, to ziarno równe: aktualny czas")]
    public string UID;

    public PlayerManager player; 

    public static System.Random random;
    public static GameplayManager ins;
    public static List<Room> rooms = new List<Room>();

    private void Awake()
    {
        ins = this;

        if (string.IsNullOrEmpty(UID) || !Application.isEditor)
            UID = DateTime.Now.ToString();

        random = new System.Random(UID.GetHashCode());
    }

    private void Start()
    {
        GetComponent<RoomsGenerator>().Setup();
    }
}
