using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIDisplay : MonoBehaviour
{
    public GameObject prefab;
    BaseItem item;

    public Sprite spriteImage;
    public string description;
    public string itemName;

    // Start is called before the first frame update
    void Start()
    {
        UpdateElement();
    }

    public ItemUIDisplay(GameObject item)
    {
        UpdateElement();
    }
    public void UpdateElement()
    {
        item = prefab.GetComponent<BaseItem>();
        if (item)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = item.icon;
            description = item.description;
            itemName = item.itemName;
        }

    }

}
