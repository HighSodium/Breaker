using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseItem : MonoBehaviour
{
    public Sprite icon;
    public string itemName;
    public string description;

    public bool canBePickedUp = true;
    public bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        icon = gameObject.GetComponent<SpriteRenderer>().sprite;
        //Debug.Assert(!itemName.Equals("") && !description.Equals(""), this.name + "needs a name and/or description!");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pickedUp)
        {
            transform.Rotate(transform.forward, 7);
        }
    }
}
