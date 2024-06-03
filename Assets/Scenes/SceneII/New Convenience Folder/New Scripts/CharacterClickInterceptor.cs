using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClickInterceptor : MonoBehaviour
{
    public NavAndAICore ai;
    public UserInputManager userInput;
    public GlobalUIManager ui;
    public bool selected = false;
    public List<string> clickableObjectTags;
    void Update()
    {
        if (userInput.DoubleClickSelf())
        {
            ui.SetUI(GlobalUIManager.UIMode.None);
            ai.ToggleWander(false);
            selected = true;
        } 
        else if (userInput.ClickSelf())
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
                if (userInput.ClickOther().gameObject.CompareTag(clickableObjectTags[i]))
                {
                    ai.MoveTo(userInput.ClickOther().gameObject.transform.position);
                }
            }
        }
    }
}
