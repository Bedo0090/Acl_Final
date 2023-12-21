using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class buy : MonoBehaviour
{
    UIDocument UiBuy;
    VisualElement inventoryUI;
    public VisualTreeAsset itemButtonTemplate;
   
    
    Label gold;
    Label errorLabel;
    public static string error="";
    private void OnEnable()
    {
        UiBuy = GetComponent<UIDocument>();
        inventoryUI = UiBuy.rootVisualElement.Q("store_buy").Q("container").Q("inventoryContainer");

        foreach (itemData item in player.inventoryItems)
        {
            slot newSlot = new slot(item, itemButtonTemplate, 0);
            if (item.type == "Ammo")
            {
                item.s = newSlot;
            }
            inventoryUI.Q("inventory").Add(newSlot.button);
        }
        gold = UiBuy.rootVisualElement.Q<Label>("gold");
        errorLabel = UiBuy.rootVisualElement.Q<Label>("error");
        errorLabel.text = error;
        gold.text = "Gold Coins: " + player.coins.ToString();
        VisualElement storeUI = UiBuy.rootVisualElement.Q("store_buy").Q("container").Q("store").Q("storeContainers");
        VisualElement store1UI = storeUI.Q("storeContainer1");
        VisualElement store2UI = storeUI.Q("storeContainer2");
        Button buy1 = store1UI.Q("1").Q<Button>();
        Button buy2 = store1UI.Q("2").Q<Button>();
        Button buy3 = store1UI.Q("3").Q<Button>();
        Button buy4 = store1UI.Q("4").Q<Button>();
        Button buy5 = store1UI.Q("5").Q<Button>();
        Button buy6 = store1UI.Q("6").Q<Button>();
        Button buy7 = store2UI.Q("1").Q<Button>();
        Button buy8 = store2UI.Q("2").Q<Button>();
        Button buy9 = store2UI.Q("3").Q<Button>();
        Button buy10 = store2UI.Q("4").Q<Button>();
        Button buy11 = store2UI.Q("5").Q<Button>();
        Button buy12 = store2UI.Q("6").Q<Button>();
        buy1.RegisterCallback<ClickEvent>(butItem1);
        buy2.RegisterCallback<ClickEvent>(butItem2);
        buy3.RegisterCallback<ClickEvent>(butItem3);
        buy4.RegisterCallback<ClickEvent>(butItem4);
        buy5.RegisterCallback<ClickEvent>(butItem5);
        buy6.RegisterCallback<ClickEvent>(butItem6);
        buy7.RegisterCallback<ClickEvent>(butItem7);
        buy8.RegisterCallback<ClickEvent>(butItem8);
        buy9.RegisterCallback<ClickEvent>(butItem9);
        buy10.RegisterCallback<ClickEvent>(butItem10);
        buy11.RegisterCallback<ClickEvent>(butItem11);
        buy12.RegisterCallback<ClickEvent>(butItem12);



    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gold.text = "Gold Coins: " + player.coins.ToString();
    }
    void butItem1(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.inventoryItems.Count == 6)
        {
            error = "You do not have space in your inventory";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 20)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.inventoryItems.Count < 6 && player.coins>=20)
        {
            itemData item = new itemData("Herb", "Green Herb", 0);
            player.inventoryItems.Add(item);
            slot newSlot = new slot(item, itemButtonTemplate,0);
            inventoryUI.Q("inventory").Add(newSlot.button);
            player.coins-= 20;
        }

    }
    void butItem2(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.inventoryItems.Count == 6)
        {
            error = "You do not have space in your inventory";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 10)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.inventoryItems.Count < 6 && player.coins>=10)
        {
            itemData item = new itemData("Herb", "Red Herb", 0);
            player.inventoryItems.Add(item);
            slot newSlot = new slot(item, itemButtonTemplate,0);
            inventoryUI.Q("inventory").Add(newSlot.button);
            player.coins -= 10;
        }
    }
    void butItem3(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.inventoryItems.Count == 6)
        {
            error = "You do not have space in your inventory";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 10)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.inventoryItems.Count < 6 && player.coins>=10)
        {
            itemData item = new itemData("Gunpowder", "Normal Gunpowder", 0);
            player.inventoryItems.Add(item);
            slot newSlot = new slot(item, itemButtonTemplate,0);
            inventoryUI.Q("inventory").Add(newSlot.button);
            player.coins -= 10;
        }
    }
    void butItem4(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.inventoryItems.Count == 6)
        {
            error = "You do not have space in your inventory";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 20)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.inventoryItems.Count < 6 && player.coins>=20)
        {
            itemData item = new itemData("Gunpowder", "High-Grade Gunpowder", 0);
            player.inventoryItems.Add(item);
            slot newSlot = new slot(item, itemButtonTemplate,0);
            inventoryUI.Q("inventory").Add(newSlot.button);
            player.coins -= 20;
        }
    }

    void butItem5(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.inventoryItems.Count == 6)
        {
            error = "You do not have space in your inventory";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 15)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.inventoryItems.Count < 6 && player.coins>=15)
        {
            itemData item = new itemData("Grenade", "Hand Grenade", 0);
            player.inventoryItems.Add(item);
            slot newSlot = new slot(item, itemButtonTemplate,0);
            inventoryUI.Q("inventory").Add(newSlot.button);
            player.coins -= 15;
        }

    }
    void butItem6(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.inventoryItems.Count == 6)
        {
            error = "You do not have space in your inventory";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 15)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.inventoryItems.Count < 6 && player.coins>=15)
        {
            itemData item = new itemData("Grenade", "Flash Grenade", 0);
            player.inventoryItems.Add(item);
            slot newSlot = new slot(item, itemButtonTemplate,0);
            inventoryUI.Q("inventory").Add(newSlot.button);
            player.coins -= 15;
        }
    }
    void butItem7(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.inventoryItems.Count == 6)
        {
            error = "You do not have space in your inventory";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 150)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.inventoryItems.Count < 6 && player.coins >= 150)
        {
            foreach (itemData itemRifle in player.inventoryItems)
            {
                if (itemRifle != null)
                    if (itemRifle.name.Equals("Assault Rifle"))
                    {
                        error = "You already have a weapon of this type";
                        errorLabel.text = error;
                        return;
                    }
            }
            foreach (itemData itemRifle in player.storageItems)
            {
                if (itemRifle != null)
                    if (itemRifle.name.Equals("Assault Rifle"))
                    {
                        error = "You already have a weapon of this type";
                        errorLabel.text = error;
                        return;
                    }
            }
            itemData item = new itemData("PW", "Assault Rifle", 30);
            player.inventoryItems.Add(item);
            slot newSlot = new slot(item, itemButtonTemplate, 0);
            inventoryUI.Q("inventory").Add(newSlot.button);
            player.coins -= 150;
        }
    }
    void butItem8(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.inventoryItems.Count == 6)
        {
            error = "You do not have space in your inventory";
            errorLabel.text = error;
            return;
        }
        if (player.coins < 140)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.inventoryItems.Count < 6 && player.coins >= 140)
        {
            foreach (itemData itemRifle in player.inventoryItems)
            {
                if (itemRifle != null)
                    if (itemRifle.name.Equals("Shotgun"))
                    {
                        error = "You already have a weapon of this type";
                        errorLabel.text = error;
                        return;
                    }
            }
            foreach (itemData itemRifle in player.storageItems)
            {
                if (itemRifle != null)
                    if (itemRifle.name.Equals("Shotgun"))
                    {
                        error = "You already have a weapon of this type";
                        errorLabel.text = error;
                        return;
                    }
            }
            itemData item = new itemData("PW", "Shotgun", 8);
            player.inventoryItems.Add(item);
            slot newSlot = new slot(item, itemButtonTemplate, 0);
            inventoryUI.Q("inventory").Add(newSlot.button);
            player.coins -= 140;
        }
    }
    void butItem9(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.coins < 30)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.coins >= 30)
        {
            itemData i = new itemData("Ammo", "Pistol Ammo", 12);
            bool putted = false;
            foreach (var item in player.inventoryItems)
            {
                if (item.name == i.name)
                {
                    item.number += i.number;
                    item.s.updateSlot(item);
                    putted = true;
                    player.coins -= 30;
                }
            }
            if (!putted&&player.inventoryItems.Count == 6)
            {
                error = "You do not have space in your inventory";
                errorLabel.text = error;
                return;
            }
            if (!putted && player.inventoryItems.Count < 6)
            {
                if (player.equippedWeapon.name.Equals("Pistol"))
                    player.equippedWeaponAmmo = i;
                player.inventoryItems.Add(i);
                slot newSlot = new slot(i, itemButtonTemplate,0);
                i.s = newSlot;
                inventoryUI.Q("inventory").Add(newSlot.button);
                player.coins -= 30;
            }
        }
        

    }
    void butItem10(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.coins < 50)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.coins >= 50)
        {
            itemData i = new itemData("Ammo", "Assault Rifle Ammo", 30);
            bool putted = false;
            foreach (var item in player.inventoryItems)
            {
                if (item.name == i.name)
                {
                    item.number += i.number;
                    item.s.updateSlot(item);
                    putted = true;
                    player.coins -= 50;
                }
            }
            if (!putted && player.inventoryItems.Count == 6)
            {
                error = "You do not have space in your inventory";
                errorLabel.text = error;
                return;
            }
            if (!putted && player.inventoryItems.Count < 6)
            {
                if (player.equippedWeapon.name.Equals("Assault Rifle"))
                    player.equippedWeaponAmmo = i;
                player.inventoryItems.Add(i);
                slot newSlot = new slot(i, itemButtonTemplate,0);
                i.s = newSlot;
                inventoryUI.Q("inventory").Add(newSlot.button);
                player.coins -= 50;
            }
        }
        
    }
    void butItem11(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.coins < 40)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.coins >= 40)
        {
            itemData i = new itemData("Ammo", "Shotgun Ammo", 8);
            bool putted = false;
            foreach (var item in player.inventoryItems)
            {
                if (item.name == i.name)
                {
                    item.number += i.number;
                    putted = true;
                    item.s.updateSlot(item);
                    player.coins -= 40;
                }
            }
            if (!putted && player.inventoryItems.Count == 6)
            {
                error = "You do not have space in your inventory";
                errorLabel.text = error;
                return;
            }
            if (!putted && player.inventoryItems.Count < 6)
            {
                if (player.equippedWeapon.name.Equals("Shotgun"))
                    player.equippedWeaponAmmo = i;
                player.inventoryItems.Add(i);
                slot newSlot = new slot(i, itemButtonTemplate,0);
                i.s = newSlot;
                inventoryUI.Q("inventory").Add(newSlot.button);
                player.coins -= 40;
            }
        }
        

    }
    void butItem12(ClickEvent c)
    {
        error = "";
        errorLabel.text = error;
        if (player.coins < 70)
        {
            error = "You do not have enough coins";
            errorLabel.text = error;
            return;
        }
        if (player.coins >= 70)
        {
            itemData i = new itemData("Ammo", "Revolver Ammo", 6);
            bool putted = false;
            foreach (var item in player.inventoryItems)
            {
                if (item.name == i.name)
                {
                    item.number += i.number;
                    item.s.updateSlot(item);
                    putted = true;
                    player.coins -= 70;
                }
            }
            if (!putted && player.inventoryItems.Count == 6)
            {
                error = "You do not have space in your inventory";
                errorLabel.text = error;
                return;
            }
            if (!putted && player.inventoryItems.Count < 6)
            {
                if (player.equippedWeapon.name.Equals("Revolver"))
                    player.equippedWeaponAmmo = i;
                player.inventoryItems.Add(i);
                slot newSlot = new slot(i, itemButtonTemplate,0);
                i.s = newSlot;
                inventoryUI.Q("inventory").Add(newSlot.button);
                player.coins -= 70;
            }
        }
        

    }
    
}




