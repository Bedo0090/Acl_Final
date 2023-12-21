using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class storage : MonoBehaviour
{
    UIDocument UiStorage;
    VisualElement inventoryUI;
    VisualElement storageUI;
    public VisualTreeAsset itemButtonTemplate;
    public static slot selectedSlot;
    public Label errorLabel;
    public static string error = "";
    Label equipedWLabel;
    Label equipedGLabel;
    string tmp;
    private void OnEnable()
    {
        UiStorage = GetComponent<UIDocument>();
        inventoryUI = UiStorage.rootVisualElement.Q("columns").Q("inventoryContainer");
        storageUI = UiStorage.rootVisualElement.Q("columns").Q("storageContainer");

        foreach (itemData item in player.inventoryItems)
        {
            slot newSlot = new slot(item, itemButtonTemplate, 0);
            if (item.type == "Ammo")
            {
                item.s = newSlot;
            }
            inventoryUI.Q("inventory").Add(newSlot.button);
        }
        foreach (itemData item in player.storageItems)
        {
            slot newSlot = new slot(item, itemButtonTemplate, 0);
            if (item.type == "Ammo")
            {
                item.s = newSlot;
            }
            storageUI.Q("storage").Add(newSlot.button);
        }

        Button move = UiStorage.rootVisualElement.Q("button").Q<Button>("move");
        errorLabel = UiStorage.rootVisualElement.Q("button").Q<Label>("error");
        equipedWLabel = UiStorage.rootVisualElement.Q("equipped").Q<Label>("equippedW");
        equipedGLabel = UiStorage.rootVisualElement.Q("equipped").Q<Label>("equippedG");
        errorLabel.text = error;
        move.RegisterCallback<ClickEvent>(moveItem);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        errorLabel.text = error;
        tmp = player.equippedGrenade == null ? "none" : player.equippedGrenade.name;
        equipedGLabel.text="Equipped Grenade: "+tmp;
        tmp = player.equippedWeapon == null ? "none" : player.equippedWeapon.name;
        equipedWLabel.text = "Equipped Weapon: " + tmp;
    }

    void moveItem(ClickEvent c)
    {
        if (selectedSlot == null)
        {
            error = "Please select an item";
            errorLabel.text = error;
            return;
        }
        itemData i = selectedSlot.item;
        if(i == player.equippedWeapon)
        {
            error = "You cannot move your currently equipped weapon";
            errorLabel.text = error;
            return;
        }
        if (player.storageItems.Contains(i))
        {
            if(player.inventoryItems.Count == 6)
            {
                error = "You do not have enough space in your inventory";
                errorLabel.text = error;
                return;
            }
            player.storageItems.Remove(i);
            storageUI.Q("storage").Remove(selectedSlot.button);
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
                    if (i.name == "Pistol Ammo" && player.equippedWeapon.name == "Pistol")
                    {
                        player.equippedWeaponAmmo = i;
                    }
                    else if (i.name == "Assault Rifle Ammo" && player.equippedWeapon.name == "Assault Rifle")
                    {
                        player.equippedWeaponAmmo = i;
                    }
                    else if (i.name == "Shotgun Ammo" && player.equippedWeapon.name == "Shotgun")
                    {
                        player.equippedWeaponAmmo = i;
                    }
                    else if (i.name == "Revolver Ammo" && player.equippedWeapon.name == "Revolver")
                    {
                        player.equippedWeaponAmmo = i;
                    }

                    player.inventoryItems.Add(i);
                    inventoryUI.Q("inventory").Add(selectedSlot.button);
                }

            }
            else
            {
                player.inventoryItems.Add(i);
                inventoryUI.Q("inventory").Add(selectedSlot.button);
            }
        }
        else if (player.inventoryItems.Contains(i))
        {
            if (i.type == "PW")
            {
                bool exist = false;
                foreach(var item in player.inventoryItems)
                {
                    if(item.type=="PW"&& item != i)
                    {
                        exist = true;
                    }
                }
                if (!exist)
                {
                    error = "You should have at least one primary weapon in your inventory";
                    errorLabel.text = error;
                    return;
                }
            }
            if (i == player.equippedWeapon)
            {
                error = "You cannot move your currently equipped weapon";
                errorLabel.text = error;
                return;
            }
            player.inventoryItems.Remove(i);
            inventoryUI.Q("inventory").Remove(selectedSlot.button);
            if (i.type == "Ammo")
            {
                bool putted = false;
                foreach (var item in player.storageItems)
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
                    player.storageItems.Add(i);
                    storageUI.Q("storage").Add(selectedSlot.button);
                }

                if (i.name == "Pistol Ammo" && player.equippedWeapon.name == "Pistol")
                {
                    player.equippedWeaponAmmo = null;
                }
                else if (i.name == "Assault Rifle Ammo" && player.equippedWeapon.name == "Assault Rifle")
                {
                    player.equippedWeaponAmmo = null;
                }
                else if (i.name == "Shotgun Ammo" && player.equippedWeapon.name == "Shotgun")
                {
                    player.equippedWeaponAmmo = null;
                }
                else if (i.name == "Revolver Ammo" && player.equippedWeapon.name == "Revolver")
                {
                    player.equippedWeaponAmmo = null;
                }
            }
            else
            {
                player.storageItems.Add(i);
                storageUI.Q("storage").Add(selectedSlot.button);
                if (i == player.equippedGrenade)
                {
                    player.equippedGrenade = null;
                }
            }
        }
        selectedSlot = null;

    }
}
