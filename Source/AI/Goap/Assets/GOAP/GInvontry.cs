using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP{
public class GInventory 
{
    List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject item){
        items.Add(item);
    }

    public GameObject FindItemWithTag(string tag){
        foreach(GameObject i in items){
            if (i.tag == tag)
                return i;
        }
        return null;
    }

    public void RemoveItem(GameObject item){
        int index = -1;
        foreach(GameObject i in items){
            index++;
            if(i == item)
                break;
        }
        if (index >= -1)
            items.RemoveAt(index);
    }
}
}
