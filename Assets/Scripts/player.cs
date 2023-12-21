using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public static List<itemData> inventoryItems=new List<itemData>();
    public static List<itemData> storageItems=new List<itemData>();
    public static int coins=500;
    public static int durability=10;
    public static int healthpoints=8;
    public static int stasispoints=0;
    public static itemData equippedWeapon;
    public static itemData equippedWeaponAmmo;
    public static itemData equippedGrenade;
    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        itemData item1 = new itemData("PW", "Pistol", 12);
        //itemData item = new itemData("PW", "Revolver",6);
        //itemData item = new itemData("PW", "Assault Rifle",30);
        //itemData item = new itemData("PW", "Shotgun",8);

        itemData item2 = new itemData("Herb", "Green Herb", 0);
        //itemData item = new itemData("Herb", "Red Herb", 0);
        //itemData item = new itemData("Herb", "Red + Red Mixture", 0);
        //itemData item = new itemData("Herb", "Green + Red Mixture", 0);
        //itemData item = new itemData("Mix", "Green + Green Mixture", 0);

        itemData item3 = new itemData("Ammo", "Pistol Ammo", 12);
        //itemData item = new itemData("Ammo", "Assault Rifle Ammo", 30);
        //itemData item = new itemData("Ammo", "Revolver Ammo", 6);
        //itemData item = new itemData("Ammo", "Shotgun Ammo", 8);

        itemData item4 = new itemData("Grenade", "Hand Grenade", 0);
        //itemData item = new itemData("Grenade", "Flash Grenade", 0);

        inventoryItems.Add(item1);
        inventoryItems.Add(item2);
        inventoryItems.Add(item3);
        inventoryItems.Add(item4);

        equippedWeapon = item1;
        equippedWeaponAmmo = item3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
