using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ATTACHED TO CHARACTER
public class CharacterClickInterceptor : MonoBehaviour
{
    public NavAndAICore ai;
    public UserInputManager userInput;
    public GlobalUIManager ui;
    public MassSelection massSelection;
    public bool selected = false;
    public List<string> clickableObjectTags;
    public bool housed = false;
    void Start()
    {
        massSelection = GameObject.FindGameObjectWithTag("MassSelector").GetComponent<MassSelection>();
        userInput = GameObject.FindGameObjectWithTag("Clicker").GetComponent<UserInputManager>();
    }
    void Update()
    {
        if (userInput.DoubleClickObj(gameObject))
        {
            ui.SetUI(GlobalUIManager.UIMode.None);
            ai.ToggleWander(false);
            selected = true;
            if (!massSelection.selectedObjs.Contains(gameObject))
                massSelection.selectedObjs.Add(gameObject);
            if (massSelection.selectedObjs.Contains(gameObject))
                massSelection.selectedObjs.Remove(gameObject);
        } 
        else if (userInput.ClickObj(gameObject))
        {
            ui.SetUI(GlobalUIManager.UIMode.Character);
        }
        if (userInput.ClickNone())
        {
            ai.ToggleWander(true);
        }
        if (selected)
        {
            for (int i = 0; i < clickableObjectTags.Count; i++)
            {
                if (userInput.OtherClickedObj().gameObject.CompareTag(clickableObjectTags[i]))
                    if (!userInput.OtherClickedObj().gameObject.CompareTag("House") || housed)
                    {
                        ai.MoveTo(userInput.OtherClickedObj().gameObject.transform.position);
                    }
            }
            if (userInput.OtherClickedObj().gameObject.CompareTag("House"))
            {
                housed = true;
                selected = false;
                ai.ToggleWander(true);
                ui.SetUI(GlobalUIManager.UIMode.None);
            }
        }
    }
}
