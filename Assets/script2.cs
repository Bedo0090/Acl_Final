using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using static UnityEditor.Progress;

public class Script2 : MonoBehaviour
{
    public StarterAssetsInputs starterAssetsInputs;

    public Button discardButton;
    public Button equipButton;
    public Text coin;
    public Text healthpoints;
    public Text stasispoints;
    List<string> itemList = new List<string> { "GreenHerb", "", "pistol", "", "", "" };
    public List<Button> buttonList;
    //String equippedWeapon;
    //String equippedGrenade;
    int counter = 0;
    public static int coinn;
    int greenHerb;
    int redHerb;
    int greenGreenMixture;
    int greenRedMixture;
    int redRedMixture;
    int normalGunpowder;
    int highGradeGunpowder;
    int handGrenade;
    int flashGrenade;
    int pistol;
    int assaultRifle;
    int shotGun;
    int revolver;
    public static int pistolAmmo = 12;
    public static int assualtRifleAmmo;
    public static int shotgunAmmo;
    public static int revolverAmmo;
    int goldBar;
    int ruby;
    int emerald;
    int healthpointss;
    int stasispointss;
    public Text resultText;
    private List<Button> selectedButtons = new List<Button>();
    public Button combineButton;      // Button to trigger the combination process
    public Button useButton;      // Button to trigger the combination process

    void Start()
    {
        coin.text = "coins:" + coinn;
        healthpoints.text = "health point:" + healthpointss;
        stasispoints.text = "stasispoints:" + stasispointss;
        // Assuming both lists have the same number of elements
        for (int i = 0; i < itemList.Count && i < buttonList.Count; i++)
        {
            Button currentButton = buttonList[i];
            Text buttonText = currentButton.GetComponentInChildren<Text>();

            if (buttonText != null)
            {
                if (itemList[i] != "")
                {
                    buttonText.text = itemList[i];
                    currentButton.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogError($"Button {i} does not have a Text component as a child.");
            }
        }
        for (int i = 0; i < itemList.Count && i < buttonList.Count; i++)
        {
            int index = i; // Create a local variable to capture the value of i
            buttonList[i].onClick.AddListener(() => OnButtonClick(index));
      
        }
       
        discardButton.onClick.AddListener(OnDiscardButtonClick);
        combineButton.onClick.AddListener(CombineSelectedItems);
        useButton.onClick.AddListener(useSelectedItems);
        equipButton.onClick.AddListener(equipSelectedItems);
    }
    void equipSelectedItems()
    {
        string item1 = selectedButtons[0].GetComponentInChildren<Text>().text;
        // Iterate through the selected buttons and remove them from the list and deactivate them

        if (item1 == "pistol")
        { 
            selectedButtons.Clear();
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;
            ThirdPersonAimController.weaponEquipped = "pistol";
        }
        else if (item1 == "assaultRifle")

        { // Clear the selected buttons list
         
            selectedButtons.Clear();
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;
            ThirdPersonAimController.weaponEquipped = "assaultrifle";

        }
        else if (item1 == "shotGun")
        { // Clear the selected buttons list
          
            selectedButtons.Clear();
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;
            ThirdPersonAimController.weaponEquipped = "shotgun";
        }
        else if (item1 == "revolver")
        { // Clear the selected buttons list

            selectedButtons.Clear();
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;
            ThirdPersonAimController.weaponEquipped = "revolver";
        }
        else if (item1 == "handGrenade")
        { // Clear the selected buttons list
     
            selectedButtons.Clear();
            PlayerGrenade.currGrenadeType = GrenadeType.Hand;
        }
        else if (item1 == "flashGrenade")
        { // Clear the selected buttons list
     
            selectedButtons.Clear();
            PlayerGrenade.currGrenadeType = GrenadeType.Flash;
        }


       


        // Clear the selected buttons list
        selectedButtons.Clear();
    }

    void useSelectedItems()
    {
        string item1 = selectedButtons[0].GetComponentInChildren<Text>().text;
        // Iterate through the selected buttons and remove them from the list and deactivate them
   
                if(item1 == "GreenHerb")
            {
                healthpointss += 2;
            }
                else if(item1 == "RedHerb")
            {
                stasispointss += 2;

            }
            else if (item1 == "GreenHerb + GreenHerb Mixture")
            {
                healthpointss += 6;
            }
            else if (item1 == "RedHerb + RedHerb Mixture")
            {
                healthpointss += 8;
            }
            else if (item1 == "GreenHerb + RedHerb Mixture")
            {
                stasispointss += 6;
            }


 
        

        // Remove selected items from the list and deactivate their buttons
        foreach (Button button in buttonList)
        {
            string str = button.GetComponentInChildren<Text>().text;

            if (button == selectedButtons[0])
            { 
                selectedButtons.Clear();
                button.gameObject.SetActive(false);
                return;
            }

        }
        foreach (string item in itemList)
        {

            if (item == item1)
            {
                // Clear the selected buttons list
                selectedButtons.Clear();
                itemList.Remove(item);
                return;
            }

        }

        // Clear the selected buttons list
        selectedButtons.Clear();
    }


    void OnButtonClick(int index)
    {
        Button clickedButton = buttonList[index];
        string itemName = itemList[index];

        if (!string.IsNullOrEmpty(itemName))
        {
            Debug.Log($"Clicked on button with item: {itemName}");

            // Add the button to the selected list
            selectedButtons.Add(clickedButton);

            // Disable the button to indicate it has been selected
            
        }
    }
    void OnDiscardButtonClick()
    {
        // Iterate through the selected buttons and remove them from the list and deactivate them
        foreach (Button selectedButton in selectedButtons)
        {
            int index = buttonList.IndexOf(selectedButton);

            // Make sure the index is valid
            if (index != -1)
            {
                // Remove the item from the list
                itemList[index] = "";

                // Deactivate the button
                selectedButton.gameObject.SetActive(false);
              
            }

        }

        // Clear the selected buttons list
        selectedButtons.Clear();
    }

    private bool CanCombine(string item1, string item2)
    {

        if (item1 == "GreenHerb" && item2 == "GreenHerb")
        {
            if (greenHerb >= 2)
            {
                greenHerb -= 2;
                greenGreenMixture++;
            }
            return true;
        }
        else if (item1 == "GreenHerb" && item2 == "RedHerb" || item2 == "GreenHerb" && item1 == "RedHerb")
        {
            if(greenHerb>0 && redHerb > 0)
            {
                greenHerb--;
                redHerb--;
                greenRedMixture++;
            }
            return true;
        }
        else if (item1 == "RedHerb  " && item2 == "RedHerb")
        {
            if (redHerb >= 2)
            {
                redHerb -= 2;
                redRedMixture++;
            }
            return true;
        }
        else if (item1 == "NormalGunpowder" && item2 == "NormalGunpowder")
        {
            if (normalGunpowder >= 2)
            {
                normalGunpowder -= 2;
                pistolAmmo += 12;
            }
            return true;
        }
        else if (item1 == "NormalGunpowder" && item2 == "High-GradeGunpowder ")
        {
            if (normalGunpowder > 0 && highGradeGunpowder > 0)
            {
                normalGunpowder--;
                highGradeGunpowder--;
                shotgunAmmo += 8;
            }
            return true;
        }
        else if (item1 == "High-GradeGunpowder " && item2 == "High-GradeGunpowder")
        {
            if (highGradeGunpowder >= 2)
            {
                highGradeGunpowder -= 2;
                assualtRifleAmmo += 30;
            }
            return true;
        }
        return false;
    }
   public void CombineSelectedItems()
    {
        if (selectedButtons.Count == 2)
        {
        
            string item1 = selectedButtons[0].GetComponentInChildren<Text>().text;
            string item2 = selectedButtons[1].GetComponentInChildren<Text>().text;

            // Check if the combination is valid
            if (CanCombine(item1, item2))
            {
                // Determine the resulting item
                string resultItem = CombineItems(item1, item2);

                // Display the result
                resultText.text = "Result: " + resultItem;

                // Remove selected items from the list and deactivate their buttons
                foreach (Button button in selectedButtons)
                {
                    itemList.Remove(button.name);
                    button.gameObject.SetActive(false);
                }

                // Activate the button with the name of the new result item
                ActivateButton(resultItem);
            }
            else
            {
                resultText.text = "Invalid combination!";
            }

            // Clear the selection
            foreach (Button button in selectedButtons)
            {
                button.image.color = Color.white;
            }
            selectedButtons.Clear();
        }
        else
        {
            resultText.text = "Select exactly two items to combine.";
        }
    }

    void ActivateButton(string itemName)
    {
        // Find the button with the specified name and activate it
        foreach (Button button in selectedButtons)
        {
            if (!button.gameObject.activeSelf)
            {
                // Assuming the button has a Text component
                Text buttonText = button.GetComponentInChildren<Text>();

                print(itemName);
                    buttonText.text = itemName;
             

                button.gameObject.SetActive(true);
                button.interactable = true;
                return;
            }
        }
    }

    string CombineItems(string item1, string item2)
    {
        // Add your combination logic here
        // For simplicity, this example just concatenates the item names
        return item1 + " + " + item2 + " Mixture";
    }


    private void Update()
    {
        coin.text = "coins:" + coinn;
        healthpoints.text = "health point:" + healthpointss;
        stasispoints.text = "stasispoints:" + stasispointss;
 
    }


}
