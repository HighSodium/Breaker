using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public float tick;
    // Start is called before the first frame update
    void Start()
    {
        tick = 8;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tick += Time.deltaTime % 360;
        transform.Rotate(transform.forward, tick);
    }
}
