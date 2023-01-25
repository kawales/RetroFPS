using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScr : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
