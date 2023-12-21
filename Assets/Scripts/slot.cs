using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class slot 
{
    public Button button;
    public itemData item;
   public slot(itemData item,VisualTreeAsset template,int price)
    {
        TemplateContainer itemButtonContainer = template.Instantiate();
        button = itemButtonContainer.Q<Button>();
        string text=item.type=="PW"|| item.type == "Ammo" ? item.name +"\n" + item.number.ToString()+" Ammo":item.name;
        text = price!=0 ? text + "\n" +"Sell for "+ "\n"+price.ToString()+" Coins" : text;
        button.text = text;
        this.item = item;
        button.RegisterCallback<ClickEvent>(onClick);
    }
    
    private void onClick(ClickEvent c)
    {
        storage.selectedSlot = this;
        sell.selectedSlot = this;
        inventory.error = "";
        storage.error = "";
        if(inventory.slectedSlot1!=null&& inventory.combineFlag == true&& inventory.slectedSlot1!=inventory.slectedSlot2)
        {
            inventory.slectedSlot2 = this;
        }
        else
        {
            inventory.slectedSlot1 = this;
        }

    }
    public void updateSlot(itemData item)
    {
        string text= item.type == "PW" || item.type == "Ammo" ? item.name + "\n" + item.number.ToString()+" Ammo" : item.name;
        this.button.text = text;
    }
}
