using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnHome : MonoBehaviour
{
    [SerializeField] private List<GameObject> selectedObjs;
    private bool clear;
    void LateUpdate()
    {
        if (clear)
        {
            selectedObjs.Clear();
            clear = false;
        }
    }
    public void CallReturnHome()
    {
        if (selectedObjs.Count > 0)
        {
            foreach (GameObject selectedObj in selectedObjs)
            {
                NavigationAndAI navigation = selectedObj.GetComponent<NavigationAndAI>();
                if (navigation != null)
                {
                    navigation.ReturnHome();
                    clear = true;
                }
            }
        }
    }
    public void AddSelectedObj(GameObject character)
    {
        if (!selectedObjs.Contains(character))
        {
            selectedObjs.Add(character);
        }
    }
    public void RemoveSelectedObj(GameObject character)
    {
        if (selectedObjs.Contains(character))
        {
            selectedObjs.Remove(character);
        }
    }
}
