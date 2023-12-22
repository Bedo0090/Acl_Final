using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public static List<itemData> inventoryItems=new List<itemData>();
    public static List<itemData> storageItems=new List<itemData>();
    public static int coins=30;
    public static int durability=10;
    public static int healthpoints=8;
    public static int stasispoints=0;
    public static itemData equippedWeapon;
    public static itemData equippedWeaponAmmo;
    public static itemData equippedGrenade;
    public static Transform playerTransform;
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

        //itemData item4 = new itemData("Grenade", "Hand Grenade", 0);
        //itemData item = new itemData("Grenade", "Flash Grenade", 0);

        inventoryItems.Add(item1);
        inventoryItems.Add(item2);
        inventoryItems.Add(item3);
        //inventoryItems.Add(item4);

        equippedWeapon = item1;
        equippedWeaponAmmo = item3;
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = GetComponent<Transform>();
        pickUp();
    }
    private void OnTriggerStay (Collider collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.CompareTag("Door"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("SpadeDoor"))
            {
                foreach (var item in player.inventoryItems)
                {
                    if (item.name == "Spade Key")
                    {
                        player.inventoryItems.Remove(item);
                        Destroy(collision.gameObject);
                    }
                }
            }
            else if (collision.gameObject.CompareTag("HeartDoor"))
            {
                foreach (var item in player.inventoryItems)
                {
                    if (item.name == "Heart Key")
                    {
                        player.inventoryItems.Remove(item);
                        Destroy(collision.gameObject);
                    }
                }
            }
            else if (collision.gameObject.CompareTag("Club Door"))
            {
                foreach (var item in player.inventoryItems)
                {
                    if (item.name == "Club Key")
                    {
                        player.inventoryItems.Remove(item);
                        Destroy(collision.gameObject);
                    }
                }
            }
            else if (collision.gameObject.CompareTag("DiamondDoor"))
            {
                foreach (var item in player.inventoryItems)
                {
                    Debug.Log(item.name);
                    if (item.name == "Diamond Key")
                    {
                        player.inventoryItems.Remove(item);
                        Destroy(collision.gameObject);
                    }
                }
            }
            else if (collision.gameObject.CompareTag("KeycardDoor"))
            {
                foreach (var item in player.inventoryItems)
                {
                    if (item.name == "Key Card")
                    {
                        player.inventoryItems.Remove(item);
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
    }
    void pickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] collidersToDamage = Physics.OverlapSphere(transform.position, 1);
            double minDistance = 0.0;
            GameObject minIndex = null;
            int i = 0;
            foreach (Collider col in collidersToDamage)
            {
                //Damage them
                if (col.gameObject.CompareTag("Green Herb") ||
                    col.gameObject.CompareTag("Red Herb") ||
                    col.gameObject.CompareTag("Revolver") ||
                    col.gameObject.CompareTag("Normal Gunpowder") ||
                    col.gameObject.CompareTag("High-Grade Gunpowder") ||
                    col.gameObject.CompareTag("Hand Grenade") ||
                    col.gameObject.CompareTag("Flash Grenade") ||
                    col.gameObject.CompareTag("Gold Bar") ||
                    col.gameObject.CompareTag("Ruby") ||
                    col.gameObject.CompareTag("Emerald") ||
                    col.gameObject.CompareTag("Emblem") ||
                    col.gameObject.CompareTag("Key Card") ||
                    col.gameObject.CompareTag("Spade Key") ||
                    col.gameObject.CompareTag("Heart Key") ||
                    col.gameObject.CompareTag("Diamond Key") ||
                    col.gameObject.CompareTag("Club Key") ||
                    col.gameObject.CompareTag("Coin"))

                {
                    double distance = Vector3.Distance(col.gameObject.transform.position, transform.position);
                    if (i == 0)
                    {
                        minDistance = distance;
                        minIndex = col.gameObject;
                    }
                    else if (distance < minDistance)
                    {
                        minDistance = distance;
                        minIndex = col.gameObject;
                    }
                    i++;

                }


            }
            if (minIndex != null)
            {
                itemData item = new itemData("", "", 0);
                if (minIndex.tag == "Green Herb")
                {
                    item = new itemData("Herb", "Green Herb", 0);
                }
                else if (minIndex.tag == "Red Herb")
                {
                    item = new itemData("Herb", "Red Herb", 0);
                }
                else if (minIndex.tag == "Revolver")
                {
                    item = new itemData("PW", "Revolver", 6);
                }
                else if (minIndex.tag == "Normal Gunpowder")
                {
                    item = new itemData("Gunpowder", "Normal Gunpowder", 0);
                }
                else if (minIndex.tag == "High-Grade Gunpowder")
                {
                    item = new itemData("Gunpowder", "High-Grade Gunpowder", 0);
                }
                else if (minIndex.tag == "Hand Grenade")
                {
                    item = new itemData("Grenade", "Hand Grenade", 0);
                }
                else if (minIndex.tag == "Flash Grenade")
                {
                    item = new itemData("Grenade", "Flash Grenade", 0);
                }
                else if (minIndex.tag == "Gold Bar")
                {
                    item = new itemData("Treasure", "Gold Bar", 0);
                }
                else if (minIndex.tag == "Ruby")
                {
                    item = new itemData("Treasure", "Ruby", 0);
                }
                else if (minIndex.tag == "Emerald")
                {
                    item = new itemData("Treasure", "Emerald", 0);
                }
                else if (minIndex.tag == "Emblem")
                {
                    item = new itemData("KI", "Emblem", 0);
                }
                else if (minIndex.tag == "Key Card")
                {
                    item = new itemData("KI", "Key Card", 0);
                }
                else if (minIndex.tag == "Spade Key")
                {
                    item = new itemData("KI", "Spade Key", 0);
                }
                else if (minIndex.tag == "Heart Key")
                {
                    item = new itemData("KI", "Heart Key", 0);
                }
                else if (minIndex.tag == "Diamond Key")
                {
                    item = new itemData("KI", "Diamond Key", 0);
                }
                else if (minIndex.tag == "Coin")
                {
                    coins += minIndex.GetComponent<CoinsObject>().amount;
                    
                    Destroy(minIndex);
                }
                else
                {
                    item = new itemData("KI", "Club Key", 0);
                }
                if (item.name != "")
                {
                    bool putted = inventory.addToInventory(item);
                    if (putted)
                    {
                        Destroy(minIndex);
                    }
                }
            }

        }

        }
    }
