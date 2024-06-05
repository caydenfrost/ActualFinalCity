using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUIManager : MonoBehaviour
{
    public UserInputManager clicker;
    //TYPES OF UI
    public GameObject CharacterUI;
    public GameObject HouseUI;
    public GameObject MainUI;
    //TYPES OF ACCESSIBLE UI
    public enum UIMode
    {
        Character,
        House,
        Main,
        None
    }
    //SETS THE SELECTED OBJECT FROM OTHER SCRIPTS
    public void SetSelectedObject(GameObject obj)
    {
        
    }
    //TURNS ON AND OFF UI PANELS
    public void SetUI(UIMode type)
    {
        if (type == UIMode.Character)
        {
            CharacterUI.SetActive(true);
            HouseUI.SetActive(false);
            MainUI.SetActive(false);
        }
        if (type == UIMode.House)
        {
            CharacterUI.SetActive(false);
            HouseUI.SetActive(true);
            MainUI.SetActive(false);
        }
        if (type == UIMode.Main)
        {
            CharacterUI.SetActive(false);
            HouseUI.SetActive(false);
            MainUI.SetActive(true);
        }
        if (type == UIMode.None)
        {
            CharacterUI.SetActive(false);
            HouseUI.SetActive(false);
            MainUI.SetActive(false);
        }
    }
}
