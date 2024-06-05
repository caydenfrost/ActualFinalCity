using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//INDEPENDENT SCRIPT
public class MassSelection : MonoBehaviour
{
    public List<GameObject> selectedObjs;
    public UserInputManager clicker;
    void Update()
    {
        if (clicker.ClickObjWTag("House") || clicker.ClickObjWTag("Rock") || clicker.ClickObjWTag("Tree"))
        {
            if (selectedObjs.Count > 0)
            {
                foreach (GameObject selectedObj in selectedObjs)
                {
                    selectedObj.GetComponent<NavAndAICore>().MoveTo(clicker.OtherClickedObj().gameObject.transform.position);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (GameObject selectedObj in selectedObjs)
            {
                selectedObj.GetComponent<NavAndAICore>().ToggleWander(true);
            }
            selectedObjs.Clear();
        }
    }
    public void AddSelectedObj(GameObject character)
    {
        if (!selectedObjs.Contains(character.gameObject))
        {
            selectedObjs.Add(character);
        }
    }
    public void RemoveSelectedObj(GameObject character)
    {
        if (selectedObjs.Contains(character.gameObject))
        {
            selectedObjs.Remove(character);
        }
    }
}
