using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class knifeRepair : MonoBehaviour
{
    UIDocument mainUi;
    Label goldLabel;
    Label errorLabel;
    Label durabilityLabel;
    Button repairButton;
    string error = "";
    
    private void OnEnable()
    {
        mainUi = GetComponent<UIDocument>();
        goldLabel=mainUi.rootVisualElement.Q("container").Q<Label>("gold");
        errorLabel = mainUi.rootVisualElement.Q("container").Q<Label>("error");
        durabilityLabel = mainUi.rootVisualElement.Q("container").Q<Label>("durability");
        durabilityLabel.text = "Durability: " + player.durability.ToString();
        goldLabel.text = "Gold: " + player.coins.ToString();
        errorLabel.text = error;
        repairButton = mainUi.rootVisualElement.Q("container").Q<Button>("repair");
        repairButton.RegisterCallback<ClickEvent>(repair);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void repair(ClickEvent c)
    {
        if (player.durability==10)
        {
            error = "Your knife durability is maximum";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 100)
        {
            error = "You do not have enough coins (100)";
        }
        if (player.coins >= 100)
        {
            player.durability = 10;
            durabilityLabel.text = "Durability: " + player.durability.ToString();
            player.coins -= 100;
            goldLabel.text = "Gold: " + player.coins.ToString();
        }
    }
}
