using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseItem : MonoBehaviour
{
    public Sprite icon;
    public string itemName;
    public string description;

    public bool isInUI;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isInUI)
        {
            transform.Rotate(transform.forward, 7);
        }
    }
}
