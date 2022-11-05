using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScr : MonoBehaviour
{
    GameObject player;
    Vector3 playerPos; // the y pos is the pos of this current object because of the lookat function
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos=new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position,playerPos,1*Time.deltaTime);
        transform.LookAt(playerPos);
    }
}
