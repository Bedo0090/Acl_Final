using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using StarterAssets;

public class inventory : MonoBehaviour
{
    UIDocument UiInventory;
    public UIDocument UiMain;
    public UIDocument buttons;
    public VisualTreeAsset itemButtonTemplate;
   
    public static slot slectedSlot1;
    public static slot slectedSlot2;
    public Button useButton;
    public Button equipButton;
    public Button combineButton;
    public Button discardButton;
    public Label healthLabel;
    public Label stasisLabel;
    public Label goldLabel;
    public Label durabilityLabel;
    public Label equipedWLabel;
    public Label equipedGLabel;
    public Label errorLabel;
    public static string error = "";

    public static bool combineFlag = false;

    public StarterAssetsInputs starterAssetsInputs;
    
    
    private void OnEnable()
    {
        UiInventory = GetComponent<UIDocument>();
        VisualElement labels = UiMain.rootVisualElement.Q("container").Q("labels");
        errorLabel = UiMain.rootVisualElement.Q("container").Q<Label>("error");
        foreach (itemData item in player.inventoryItems)
        {

            slot newSlot = new slot(item, itemButtonTemplate, 0);
            UiInventory.rootVisualElement.Q("ItemRow").Add(newSlot.button);
        }

        VisualElement tmp = buttons.rootVisualElement.Q("holder");

        useButton = tmp.Q<Button>("use");
        equipButton = tmp.Q<Button>("equip");
        discardButton = tmp.Q<Button>("discard");
        combineButton = tmp.Q<Button>("combine");

        useButton.RegisterCallback<ClickEvent>(useSelectedItems);
        equipButton.RegisterCallback<ClickEvent>(equipSelectedItems);
        combineButton.RegisterCallback<ClickEvent>(setCompineFlag);
        discardButton.RegisterCallback<ClickEvent>(discard);

        healthLabel = labels.Q<Label>("health");
        stasisLabel = labels.Q<Label>("stasis");
        goldLabel = labels.Q<Label>("gold");
        durabilityLabel = labels.Q<Label>("durability");
        equipedGLabel = labels.Q<Label>("equipedG");
        equipedWLabel = labels.Q<Label>("equipedW");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldLabel.text ="Gold: "+ player.coins.ToString();
        healthLabel.text ="Health: "+ player.healthpoints.ToString();
        stasisLabel.text ="stasis: "+ player.stasispoints.ToString();
        durabilityLabel.text ="durability: "+ player.durability.ToString();
        string text = player.equippedGrenade == null ? "none" : player.equippedGrenade.name;
        equipedGLabel.text ="Equipped Grenade: "+text;
        text = player.equippedWeapon == null ? "none" : player.equippedWeapon.name;
        equipedWLabel.text ="Equipped Weapon: "+text;
        errorLabel.text = error;
        if (slectedSlot2 != null)
        {
            combine();
            slectedSlot1 = null;
            slectedSlot2 = null;
        }
    }
    void equipSelectedItems(ClickEvent c)
    {
        if (slectedSlot1 == null)
        {
            error = "Please select an item";
            errorLabel.text = error;
            return;
        }
        itemData item = slectedSlot1.item;
        // Iterate through the selected buttons and remove them from the list and deactivate them

        if (item.type!="PW"&& item.type!="Grenade")
        {
            slectedSlot1 = null;
            error = "You can only equip primary weapons or grenades";
            errorLabel.text = error;
            return;
        }
        else if (item.type == "PW")

        {
            starterAssetsInputs.shoot = false;
            starterAssetsInputs.reload = false;
            player.equippedWeapon = item;
            foreach(var needed in player.inventoryItems)
            {
                if (needed.type == "Ammo")
                {
                    if(needed.name=="Pistol Ammo"&& item.name == "Pistol")
                    {
                        player.equippedWeaponAmmo = needed;
                    }
                    else if(needed.name == "Assault Rifle Ammo" && item.name == "Assault Rifle")
                    {
                        player.equippedWeaponAmmo = needed;
                    }
                    else if (needed.name == "Shotgun Ammo" && item.name == "Shotgun")
                    {
                        player.equippedWeaponAmmo = needed;
                    }
                    else if (needed.name == "Revolver Ammo" && item.name == "Revolver")
                    {
                        player.equippedWeaponAmmo = needed;
                    }
                    else
                    {
                        player.equippedWeaponAmmo = null;
                    }
                }
            }
        }
        else if (item.type == "Grenade")

        {
            player.equippedGrenade = item;
            if (player.equippedGrenade.name.Equals("Hand Grenade"))
                PlayerGrenade.EquipHandGrenade();
            else if (player.equippedGrenade.name.Equals("Flash Grenade"))
                PlayerGrenade.EquipFlashGrenade();
        }

        string text = player.equippedGrenade == null ? "none" : player.equippedGrenade.name;
        equipedGLabel.text = "Equipped Grenade: " + text;
        text = player.equippedWeapon == null ? "none" : player.equippedWeapon.name;
        equipedWLabel.text = "Equipped Weapon: " + text;
        slectedSlot1 = null;
        slectedSlot2 = null;
       
       
    }
    void useSelectedItems(ClickEvent c)
    {
        if (slectedSlot1 == null)
        {
            error = "Please select an item";
            errorLabel.text = error;
            return;
        }
        itemData item = slectedSlot1.item;
        if (item.type != "Herb" & item.type != "Mix")
        {
            error = "You can only use herbs or mixtures";
            errorLabel.text = error;
            slectedSlot1 = null;
            return;
        }

        // Iterate through the selected buttons and remove them from the list and deactivate them

        if (item.name == "Green Herb")
        {
            if ((player.healthpoints += 2) >= 8)
            {
                player.healthpoints = 8;
                PlayerHealth.change = true;
            }
            else
            {
                player.healthpoints += 2;
                PlayerHealth.change = true;
            }
        }
        else if (item.name == "Red Herb")
        {
            player.stasispoints += 2;

        }
        else if (item.name == "Green + Green Mixture")
        {
            if ((player.healthpoints += 6) >= 8)
            {
                player.healthpoints = 8;
                PlayerHealth.change = true;
            }
            else
            {
                player.healthpoints += 6;
                PlayerHealth.change = true;
            }
        }
        else if (item.name == "Red + Red Mixture")
        {
            player.stasispoints += 6;
        }
        else if (item.name == "Green + Red Mixture")
        {
            player.healthpoints = 8;
            PlayerHealth.change = true;
        }
        UiInventory.rootVisualElement.Q("ItemRow").Remove(slectedSlot1.button);
        player.inventoryItems.Remove(slectedSlot1.item);
        goldLabel.text = "Gold: " + player.coins.ToString();
        healthLabel.text = "Health: " + player.healthpoints.ToString();
        stasisLabel.text = "stasis: " + player.stasispoints.ToString();
        
        slectedSlot1 = null;
        slectedSlot2 = null;
        
        //destroy slot and delete item


    }
    public void combine()
    {
        itemData item1 = slectedSlot1.item;
        itemData item2 = slectedSlot2.item;
        if (item1 == item2)
        {
            error = "Cannot combine selected items";
            errorLabel.text = error;
            slectedSlot1 = null;
            slectedSlot2 = null;
            return;
        }
        bool canCompine = false;
        itemData resultedItem=null;
        slot resultedSlot=null;
        bool mawgood = false;
        if (item1.name == "Green Herb" && item2.name == "Green Herb")
        {
            resultedItem = new itemData("Mix", "Green + Green Mixture", 0);
            resultedSlot = new slot(resultedItem, itemButtonTemplate,0);
            canCompine= true;
        }
        else if (item1.name == "Green Herb" && item2.name == "Red Herb" || item2.name == "Green Herb" && item1.name == "Red Herb")
        {
            resultedItem = new itemData("Mix", "Green + Red Mixture", 0);
            resultedSlot = new slot(resultedItem, itemButtonTemplate,0);
            canCompine = true;
        }
        else if (item1.name == "Red Herb" && item2.name == "Red Herb")
        {
            resultedItem = new itemData("Mix", "Red + Red Mixture", 0);
            resultedSlot = new slot(resultedItem, itemButtonTemplate,0);
            canCompine = true;
        }
        else if (item1.name == "Normal Gunpowder" && item2.name == "Normal Gunpowder")
        {
            resultedItem = new itemData("Ammo", "Pistol Ammo", 12);
            foreach (var item in player.inventoryItems)
            {
                if (item.name == resultedItem.name)
                {
                    item.number += resultedItem.number;
                    item.s.updateSlot(item);
                    mawgood = true;
                }
            }
            if (mawgood == false)
            {
                resultedSlot = new slot(resultedItem, itemButtonTemplate, 0);
                if (player.equippedWeapon.name.Equals("Pistol"))
                    player.equippedWeaponAmmo = resultedItem;
            }
            canCompine = true;
        }
        else if (item1.name == "Normal Gunpowder" && item2.name == "High-Grade Gunpowder" || item2.name == "Normal Gunpowder" && item1.name == "High-Grade Gunpowder")
        {
            resultedItem = new itemData("Ammo", "Shotgun Ammo", 8);
            foreach (var item in player.inventoryItems)
            {
                if (item.name == resultedItem.name)
                {
                    item.number += resultedItem.number;
                    item.s.updateSlot(item);
                    mawgood = true;
                }
            }
            if (mawgood == false)
            {
                resultedSlot = new slot(resultedItem, itemButtonTemplate, 0);
                if (player.equippedWeapon.name.Equals("Shotgun"))
                    player.equippedWeaponAmmo = resultedItem;
            }
            canCompine = true;
        }
        else if (item1.name == "High-Grade Gunpowder" && item2.name == "High-Grade Gunpowder")
        {
            resultedItem = new itemData("Ammo", "Assault Rifle Ammo", 30);
            foreach (var item in player.inventoryItems)
            {
                if (item.name == resultedItem.name)
                {
                    item.number += resultedItem.number;
                    item.s.updateSlot(item);
                    mawgood = true;
                }
            }
            if (mawgood == false)
            {
                resultedSlot = new slot(resultedItem, itemButtonTemplate, 0);
                if (player.equippedWeapon.name.Equals("Assault Rifle"))
                    player.equippedWeaponAmmo = resultedItem;
            }
            canCompine = true;
        }
        if (canCompine)
        {
            UiInventory.rootVisualElement.Q("ItemRow").Remove(slectedSlot1.button);
            player.inventoryItems.Remove(slectedSlot1.item);
            UiInventory.rootVisualElement.Q("ItemRow").Remove(slectedSlot2.button);
            player.inventoryItems.Remove(slectedSlot2.item);
            if (mawgood == false)
            {
                UiInventory.rootVisualElement.Q("ItemRow").Add(resultedSlot.button);
                player.inventoryItems.Add(resultedItem);
            }
            
        }
        else
        {
            error = "Cannot combine selected items";
            errorLabel.text = error;
        }
        slectedSlot1 = null;
        slectedSlot2 = null;
        combineFlag = false;

    }
    void setCompineFlag(ClickEvent c)
    {
        if (slectedSlot1 != null)
        {
            combineFlag = true;
        }
       
    }
    void discard(ClickEvent c)
    {
        if (slectedSlot1 == null)
        {
            error = "Please select an item";
            errorLabel.text = error;
            return;
        }
        itemData i = slectedSlot1.item;
        if (i.type == "PW" || i.type == "KI")
        {
            slectedSlot1 = null;
            error = "You cannot discard primary weapons or key-items";
            errorLabel.text = error;
            return;
        }
        if (i.type == "Grenade")
            player.equippedGrenade = null;
        UiInventory.rootVisualElement.Q("ItemRow").Remove(slectedSlot1.button);
        player.inventoryItems.Remove(slectedSlot1.item);
        slectedSlot1 = null;
    }

    public static bool addToInventory(itemData i)
    {
        if (player.inventoryItems.Count == 6)
        {
            return false;
        }
        if (i.type == "Ammo")
        {
            bool putted = false;
            foreach (var item in player.inventoryItems)
            {
                if (item.name == i.name)
                {
                    item.number += i.number;
                    item.s.updateSlot(item);
                    putted = true;
                }
            }
            if (!putted)
            {
                player.inventoryItems.Add(i);

            }

            return true;
        }
        else
        {
            player.inventoryItems.Add(i);
            return true;
        }
    }
}
