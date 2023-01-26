using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeScr : EnemyAbs
{
    GameObject player;
    Vector3 playerPos; // the y pos is the pos of this current object because of the lookat function
    float dist; 
    [SerializeField]GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dist = Vector3.Distance(this.transform.position,player.transform.position);
        if(dist <= attackDistance && !attacking)
        {
            GetComponent<Animator>().SetBool("move",false);
            playerPos=new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position,playerPos,1*Time.deltaTime*speed);
            transform.LookAt(playerPos);
            StartCoroutine(enemyAttack());
        }
        else if(dist <= noticeDistance && !attacking)
        {
            GetComponent<Animator>().SetBool("move",true);
            playerPos=new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position,playerPos,1*Time.deltaTime*speed);
            transform.LookAt(playerPos);
        }
        else
        {
            GetComponent<Animator>().SetBool("move",false);
        }

        
    }

    public override void enemyHit(int damage)
    {
        health-=damage;
        GetComponent<Animator>().Play("Base Layer.Damage",0,0.25f);
        if(health<=0)
            enemyDie();
    }

    public override IEnumerator enemyAttack()
    {
        attacking = true;
        GetComponent<Animator>().SetBool("attack",true);
        yield return new WaitForSeconds(0.5f);
        // ako je igrac jos uvek unutar distance za napad oduzeti health
        Instantiate(bullet,this.transform.position,this.transform.rotation);
        GetComponent<Animator>().SetBool("attack",false);
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }


}

