using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PuzzleData : ScriptableObject
{
    public StoragesInfo[] storages;    // Tablica obiektów w których są itemy
}

[Serializable]
public class StoragesInfo
{
    public OpenType openType;

    public bool closePreviousObject;
    public GameObject[] itemsPrefabs;
}

public enum OpenType
{
    Open, Closed, NoMatter
}

