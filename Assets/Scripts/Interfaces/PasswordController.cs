using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PasswordController : MonoBehaviour
{
    public static PasswordController ins;

    private RoomObject roomObject;
    private InputField[] inputs;
    private CanvasGroup cG;
    private int activInput = 0;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        inputs = transform.GetComponentsInChildren<InputField>();
        cG = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && cG.alpha == 1)
        {
            activInput++;
            if (activInput >= inputs.Length) activInput = 0;
            inputs[activInput].ActivateInputField();
        }

        if (Input.GetKeyUp(KeyCode.Return) && cG.alpha == 1)
        {
            btnAccept_Click();
        }

        if (Input.GetKeyUp(KeyCode.Escape) && cG.alpha == 1)
        {
            btnCancle_Click();
        }
    }

    public void SetUp(RoomObject roomObject)
    {
        this.roomObject = roomObject;
        GameplayManager.ins.player.GetComponent<PlayerController>().enabled = false;

        foreach (InputField item in inputs)
            item.text = "";

        inputs[activInput = 0].ActivateInputField();

        cG.alpha = 1;
        cG.blocksRaycasts = true;
    }

    public void btnCancle_Click()
    {
        GameplayManager.ins.player.GetComponent<PlayerController>().enabled = true;

        cG.alpha = 0;
        cG.blocksRaycasts = false;
    }

    public void btnAccept_Click()
    {
        string pass = "";
        foreach (InputField item in inputs)
            pass += item.text;

        pass = pass.ToUpper();

        if (roomObject.passwordCode.Equals(pass))
        {
            roomObject.locked = false;

            if (roomObject is Door && ((Door)roomObject).roomUnlock != null) ((Door)roomObject).roomUnlock.Available = true;

            ObjectOptionManager.ins.SetUpPanel(ObjectsDetector.ins.selectedID);

            var item = InterfaceManager.ins.ItemSlots.FirstOrDefault(x =>
                                               (x.transform.childCount == 1)
                                            && (x.transform.GetChild(0).GetComponent<Item>() is Note)
                                            && (((Note)x.transform.GetChild(0).GetComponent<Item>()).password == pass));

            if (item != null) Destroy(item.transform.GetChild(0).gameObject);
            btnCancle_Click();
        }
        else
        {
            activInput = 0;
            inputs[activInput].ActivateInputField();
            PanelInfoManager.ins.AddInfo(InfoColor.Red, "Podane hasło nie pasuje", "Próba odblokowania obiektu");
           // Debug.Log("Podane hasło nie pasuje.");
        }
    }
}
