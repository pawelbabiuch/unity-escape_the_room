using UnityEngine;
using UnityEditor;

public class PuzzleDataAsset
{
    [MenuItem("Assets/Create/PuzzleData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<PuzzleData>();
    }
}