using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject followTarget;
    public float followSpeed = 15f;
    public float zoomHeight = 10;

    [HideInInspector] public float mouseWorldLocation;



    public Vector2 originOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followTarget == null) return;
        Vector2 followPos = followTarget.transform.position;
        Vector3 finalPos = new Vector3(followPos.x, followPos.y, -zoomHeight);

        transform.position = Vector3.Lerp(transform.position, finalPos, followSpeed * Time.deltaTime);


    }
}
