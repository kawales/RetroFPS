using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScr : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,player.transform.position,1*Time.deltaTime);
    }
}
