using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObject : MonoBehaviour, IUnlockable
{
    [DisableWhenRun]
    public string objectName;
    private string description;

    private bool Locked;
    private LockedType Lockedtype;

    private int KeyID;
    private string PasswordCode;
    private ObjectType Objecttype;


    public int keyID
    {
        get { return KeyID; }
        set { KeyID = value; }
    }

    public bool locked
    {
        get { return Locked; }
        set { Locked = value; }
    }

    public LockedType lockedType
    {
        get { return Lockedtype; }
        set { Lockedtype = value; }
    }

    public ObjectType objectType
    {
        get { return Objecttype; }
        set { Objecttype = value; }
    }

    public string passwordCode
    {
        get { return PasswordCode; }
        set { PasswordCode = value; }
    }

    public virtual void Unlock()
    {
        switch(lockedType)
        {
            case LockedType.Key:

                Transform itemTransform = InterfaceManager.ins.inUseItem;

                if(!itemTransform)
                {
                    PanelInfoManager.ins.AddInfo(InfoColor.Orange, "Brak przedmiotu, którego mógłbyś użyć");
                   // Debug.Log("Brak przedmiotu, którego mógłbyś użyć");
                }
                else
                {
                    Item item = itemTransform.GetComponent<Item>();

                    if (item is Key && ((Key)item).keyID == this.keyID)
                    {
                        locked = false;

                        if (this is Door && ((Door)this).roomUnlock != null) ((Door)this).roomUnlock.Available = true;

                        ObjectOptionManager.ins.SetUpPanel(ObjectsDetector.ins.selectedID);
                        Destroy(itemTransform.gameObject);
                        PanelInfoManager.ins.AddInfo(InfoColor.Green, string.Format("Odblokowałeś obiekt za pomocą {0}!", item.itemName));
                        //Debug.Log("Odblokowałeś obiekt!");
                    }
                    else
                    {
                        PanelInfoManager.ins.AddInfo(InfoColor.Red, "Posiadny przedmiot nie pasuje do zamka");
                      //  Debug.Log("Posiadny przedmiot nie pasuje do zamka");
                    }
                }

                break;
            case LockedType.Object:
                break;
            case LockedType.Password:
                PasswordController.ins.SetUp(this);
                break;
        }
    }

    public virtual void SetUpObject(bool locked, ObjectType objectType)
    {
        this.lockedType = LockedType.Object;
        this.locked = locked;
        this.objectType = objectType;
    }

    public virtual void SetUpObject(bool locked, Key key)
    {
        this.lockedType = LockedType.Key;
        this.locked = locked;
        this.keyID = key.keyID;
        this.description = string.Format("{0} zamek w {1} kolorze; {2} mechanizm.", key.lock01, key.lock02, key.lock03);
    }

    public virtual void SetUpObject(bool locked, Note note)
    {
        this.lockedType = LockedType.Password;
        this.locked = locked;
        this.passwordCode = note.password;

        this.description = string.Format("{1} kawałek potarganej kartki w {2} kolorze leży obok {0}.", objectName, note.lock01, note.lock02);
    }

    public virtual void SetUpObject()
    {
        this.lockedType = LockedType.None;
        this.locked = false;
    }

    private void OnMouseEnter()
    {
        if(this.locked)
            RoomDescription.ins.SetUp(objectName, description);
        else
        {
            if(this is Storage)
            {
                Storage storage = GetComponent<Storage>();
                
                if(storage.items.Count == 0)
                    RoomDescription.ins.SetUp(objectName, "Pusto");
                else
                    RoomDescription.ins.SetUp(objectName, "Otwarty");
            }
            else if(this is Door)
            {
                RoomDescription.ins.SetUp(objectName, "Drzwi otwarte");
            }
        }
    }

    private void OnMouseExit()
    {
        RoomDescription.ins.Disable();
    }
}
