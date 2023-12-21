using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="item")]
public class itemData :ScriptableObject
{
    public string type;
    public string name;
    public int number;
    public slot s;

    public itemData(string type,string name,int number)
    {
        this.type = type;
        this.name=name;
        this.number = number;
    }
    



}
