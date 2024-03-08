using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateDetails : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text ageText;
    public TMP_Text killsText;
    public TMP_Text childrenText;
    public TMP_Text strengthText;
    public TMP_Text healthText;
    public TMP_Text intelText;
    public TMP_Text speedText;
    public TMP_Text ssText;
    public TMP_Text pvText;
    public TMP_Text religionText;
    public TMP_Text originText;
    public TMP_Text villageText;
    public TMP_Text kingdomText;

    // Update is called once per frame
    public void UpdateDetailPanel(CharacterData data)
    {
        nameText.text = data.name;
        ageText.text = data.age.ToString();
        killsText.text = data.kills.ToString();
        childrenText.text = data.children.ToString();
        strengthText.text = data.strength.ToString();
        healthText.text = data.health.ToString();
        intelText.text = data.intelligence.ToString();
        speedText.text = data.speed.ToString();
        ssText.text = data.politicalStatus;
        pvText.text = data.politicalView;
        religionText.text = data.religion;
        originText.text = data.placeOfOrigin;
        villageText.text = data.village;
        kingdomText.text = data.kingdom;
    }
}
