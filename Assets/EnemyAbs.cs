using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbs : MonoBehaviour
{
    [SerializeField]protected int health=5;
    [SerializeField]protected float speed=1;
    [SerializeField]protected float noticeDistance=5;
    [SerializeField]protected int damage=1;
    [SerializeField]protected float attackCooldown;
    [SerializeField]protected float attackDistance;
    protected bool attacking; // Trenutno napada
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void enemyHit(int damage);

    public virtual void enemyDie()
    {
        GetComponent<Animator>().SetBool("dead",true);
        speed=0;
        damage=0;
        attackDistance=0;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(this.gameObject,10);
    }

    public abstract IEnumerator enemyAttack();
}
