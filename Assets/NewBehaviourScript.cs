
using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class NewBehaviourScript : MonoBehaviour
{
    List<string> myInventory = new() { "greenHerb", "", "", "", "", "" };
    public Text coin;
    public Text healthpoints;
    public Text stasispoints;
    public Text knife;
    public Text weapon;
    public Text itemOne;
    public Text itemTwo;
    public Text itemThree;
    public Text itemFour;
    public Text itemFive;
    public Text itemSix;




    public Text grenade;
    int counter = 0;
    int coinn;
    int greenHerbb = 1;
    int healthpointss ;
    int stasispointss ;
    int knifee = 30 ;
    String weaponn;
    int ammo;
    String grenadee  ;
    public Button grenadeequip;
    public Button weaponequip;
    public Button use;
    public Button discard;

    public Button grenadeequip2;
    public Button weaponequip2;
    public Button use2;
    public Button discard2;

    public Button grenadeequip3;
    public Button weaponequip3;
    public Button use3;
    public Button discard3;

    public Button grenadeequip4;
    public Button weaponequip4;
    public Button use4;
    public Button discard4;

    public Button grenadeequip5;
    public Button weaponequip5;
    public Button use5;
    public Button discard5;

    public Button grenadeequip6;
    public Button weaponequip6;
    public Button use6;
    public Button discard6;


    void Start()
    {
        foreach (string item in myInventory)
        {
            // Check your condition using if statement
            if (itemOne.Equals(""))
            {
                if (item.Equals("Hand Grenade") || item.Equals("Flash Grenade")){
                    
                    grenadeequip.interactable = true;
                    discard.interactable = true;

}
                else if (item.Equals("Pistol") || item.Equals("Assault Rifle") || item.Equals("Shotgun")|| item.Equals("Revolver") )
                    {
                    weaponequip.interactable = true;
                }
                else if (item.Equals("green Herb") || item.Equals("red Herb") || item.Equals("greengreenmixture") || item.Equals("redgreenmixture")|| item.Equals("redredmixture"))
                {
                    use.interactable = true;
                    discard.interactable = true;
                }
                itemOne.text += item + "\n";
            }
            if (itemTwo.Equals(""))
            {
                if (item.Equals("Hand Grenade") || item.Equals("Flash Grenade"))
                {
                    discard2.interactable = true;
                    grenadeequip2.interactable = true;
                }
                else if (item.Equals("Pistol") || item.Equals("Assault Rifle") || item.Equals("Shotgun") || item.Equals("Revolver"))
                {
                    weaponequip2.interactable = true;
                }
                else if (item.Equals("green Herb") || item.Equals("red Herb") || item.Equals("greengreenmixture") || item.Equals("redgreenmixture") || item.Equals("redredmixture"))
                {
                    discard2.interactable = true;
                    use2.interactable = true;
                }
                itemTwo.text += item + "\n";
            }
            if (itemThree.Equals(""))
            {
                if (item.Equals("Hand Grenade") || item.Equals("Flash Grenade"))
                {
                    discard3.interactable = true;
                    grenadeequip3.interactable = true;
                }
                else if (item.Equals("Pistol") || item.Equals("Assault Rifle") || item.Equals("Shotgun") || item.Equals("Revolver"))
                {
                    weaponequip3.interactable = true;
                }
                else if (item.Equals("green Herb") || item.Equals("red Herb") || item.Equals("greengreenmixture") || item.Equals("redgreenmixture") || item.Equals("redredmixture"))
                {
                    discard3.interactable = true;
                    use3.interactable = true;
                }
                itemThree.text += item + "\n";
            }
            if (itemFour.Equals(""))
            {
                if (item.Equals("Hand Grenade") || item.Equals("Flash Grenade"))
                {
                    discard4.interactable = true;
                    grenadeequip4.interactable = true;
                }
                else if (item.Equals("Pistol") || item.Equals("Assault Rifle") || item.Equals("Shotgun") || item.Equals("Revolver"))
                {
                    weaponequip4.interactable = true;
                }
                else if (item.Equals("green Herb") || item.Equals("red Herb") || item.Equals("greengreenmixture") || item.Equals("redgreenmixture") || item.Equals("redredmixture"))
                {
                    discard4.interactable = true;
                    use4.interactable = true;
                }
                itemFour.text += item + "\n";
            }
            if (itemFive.Equals(""))
            {
                if (item.Equals("Hand Grenade") || item.Equals("Flash Grenade"))
                {
                    discard5.interactable = true;
                    grenadeequip5.interactable = true;
                }
                else if (item.Equals("Pistol") || item.Equals("Assault Rifle") || item.Equals("Shotgun") || item.Equals("Revolver"))
                {
                    weaponequip5.interactable = true;
                }
                else if (item.Equals("green Herb") || item.Equals("red Herb") || item.Equals("greengreenmixture") || item.Equals("redgreenmixture") || item.Equals("redredmixture"))
                {
                    discard5.interactable = true;
                    use5.interactable = true;
                }
                itemFive.text += item + "\n";
            }
            if (itemSix.Equals(""))
            {
                if (item.Equals("Hand Grenade") || item.Equals("Flash Grenade"))
                {
                    discard6.interactable = true;
                    grenadeequip6.interactable = true;
                }
                else if (item.Equals("Pistol") || item.Equals("Assault Rifle") || item.Equals("Shotgun") || item.Equals("Revolver"))
                {
                    weaponequip6.interactable = true;
                }
                else if (item.Equals("green Herb") || item.Equals("red Herb") || item.Equals("greengreenmixture") || item.Equals("redgreenmixture") || item.Equals("redredmixture"))
                {
                    discard6.interactable = true;
                    use6.interactable = true;
                }
                itemSix.text += item + "\n";
            }
        }
    
    // Example string you want to display

    coinn = 30;
        greenHerbb = 1;
        healthpointss =30;
        weaponn = "pistol";
        ammo = 12;
        coin.text = "coins:" + coinn;
        healthpoints.text = "health point:" + healthpointss;
        stasispoints.text = "stasispoints:" + stasispointss;
        weapon.text = "weapon:" + weaponn + " ammo" + ammo;

    }

    private void Update()
    {    
        coin.text = "coins:"+coinn;
        healthpoints.text = "health point:"+healthpointss;
        stasispoints.text = "stasispoints:" + stasispointss;
        knife.text = "knife:"+ knifee;
        weapon.text = "weapon:"+weaponn+" ammo"+ammo;
        grenade.text = "grenade:"+grenadee;
    }







}
