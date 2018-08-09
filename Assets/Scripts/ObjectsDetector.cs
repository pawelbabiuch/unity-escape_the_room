using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDetector : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Lista obiektów wykrytych przez gracza")]
    public List<RoomObject> objectsDetected = new List<RoomObject>();
    public int selectedID { get; set;}
    private sbyte scroll;

    public static ObjectsDetector ins;

    private void Awake()
    {
        ins = this;
        selectedID = 0;
    }

    private void Update()
    {
        scroll = (sbyte)Mathf.Clamp(Input.GetAxisRaw("Mouse ScrollWheel"), -1, 1);

        if(objectsDetected.Count > 0 && scroll != 0)
        {
            selectedID += scroll;

            if (selectedID < 0)
                selectedID = objectsDetected.Count - 1;
            else if (selectedID > objectsDetected.Count - 1)
                selectedID = 0;

            ObjectOptionManager.ins.SetUpPanel(selectedID);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsDetected.Add(other.GetComponent<RoomObject>());
        ObjectOptionManager.ins.SetUpPanel(selectedID);
    }

    private void OnTriggerExit(Collider other)
    {
        objectsDetected.Remove(other.GetComponent<RoomObject>());
        selectedID = 0;
    }

    /// <summary>
    /// Zwraca aktualy wybrany obiekt.
    /// </summary>
    public RoomObject currentObject
    {
        get
        {
            if (objectsDetected.Count > 0)
                return objectsDetected[selectedID];
            else
                return null;
        }
    }
}
