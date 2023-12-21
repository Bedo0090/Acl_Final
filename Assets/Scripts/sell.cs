using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class sell : MonoBehaviour
{
    UIDocument UiSell;
    VisualElement inventoryUI;
    public VisualTreeAsset itemButtonTemplate;
    public VisualTreeAsset itemLabelTemplate;
    

    Label gold;
    public static slot selectedSlot;
    private void OnEnable()
    {
        UiSell = GetComponent<UIDocument>();
        inventoryUI = UiSell.rootVisualElement.Q("store_sell").Q("inventoryContainer");

        foreach (itemData item in player.inventoryItems)
        {
            if (item.type == "Herb" || item.type == "Mix" || item.type == "Grenade" || item.type == "Gunpowder" || item.type == "Treasure")
            {
                int price = getPrice(item);
                slot newSlot = new slot(item, itemButtonTemplate, price);
                inventoryUI.Q("inventory_sellable").Add(newSlot.button);
            }
            else
            {
                TemplateContainer templateContainer = itemLabelTemplate.Instantiate();
                Label newLabel = templateContainer.Q<Label>();
                string text = item.type == "PW" || item.type == "Ammo" ? item.name + " " + item.number.ToString() : item.name;
                newLabel.text = text;
                inventoryUI.Q("inventory_nonSellable").Add(newLabel);
            }

        }
        gold = inventoryUI.Q<Label>("gold");
        gold.text = "Gold Coins: " + player.coins.ToString();
        Button sell = UiSell.rootVisualElement.Q<Button>("sell");
        sell.RegisterCallback<ClickEvent>(sellItem);


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
    void sellItem(ClickEvent c)
    {
        itemData item = selectedSlot.item;
        player.inventoryItems.Remove(item);
        inventoryUI.Q("inventory_sellable").Remove(selectedSlot.button);
        if (item.name == "Green Herb")
        {
            player.coins += 15;
        }
        else if (item.name == "Red Herb")
        {
            player.coins += 5;
        }
        else if (item.name == "Green + Green Mixture")
        {
            player.coins += 30;
        }
        else if (item.name == "Green + Red Mixture")
        {
            player.coins += 20;
        }
        else if (item.name == "Red + Red Mixture")
        {
            player.coins += 10;
        }
        else if (item.name == "Normal Gunpowder")
        {
            player.coins += 5;
        }
        else if (item.name == "High-Grade Gunpowder")
        {
            player.coins += 15;
        }
        else if (item.name == "Hand Grenade")
        {
            if (player.equippedGrenade == item)
                player.equippedGrenade = null;
            player.coins += 10;
        }
        else if (item.name == "Flash Grenade")
        {
            if (player.equippedGrenade == item)
                player.equippedGrenade = null;
            player.coins += 10;
        }
        else if (item.name == "Gold Bar")
        {
            player.coins += 100;
        }
        else if (item.name == "Ruby")
        {
            player.coins += 200;
        }
        else if (item.name == "Emerald")
        {
            player.coins += 500;
        }

    }
    int getPrice(itemData item)
    {
        int result = 0;
        if (item.name == "Green Herb")
        {
            result= 15;
        }
        else if (item.name == "Red Herb")
        {
            result= 5;
        }
        else if (item.name == "Green + Green Mixture")
        {
            result= 30;
        }
        else if (item.name == "Green + Red Mixture")
        {
            result= 20;
        }
        else if (item.name == "Red + Red Mixture")
        {
            result= 10;
        }
        else if (item.name == "Normal Gunpowder")
        {
            result= 5;
        }
        else if (item.name == "High-Grade Gunpowder")
        {
            result= 15;
        }
        else if (item.name == "Hand Grenade")
        {
            result= 10;
        }
        else if (item.name == "Flash Grenade")
        {
            result= 10;
        }
        else if (item.name == "Gold Bar")
        {
            result= 100;
        }
        else if (item.name == "Ruby")
        {
            result= 200;
        }
        else if (item.name == "Emerald")
        {
            result= 500;
        }
        return result;
    }
    
}
