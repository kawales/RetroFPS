using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScr : MonoBehaviour
{
    [Header("Speed stats")]
    [SerializeField] float rotAngleSpeed=1.3f;
    public float playerSpeed=3f;
    public float speedBoost=2f;
    public int activeBoost=0;
    [Header("Weapon stuff")]
    int selectedWeapon=0;
    int selectedWeaponDamage = 1;
    [SerializeField]GameObject currentMuzzle;
    [SerializeField]int health = 5;
    // Start is called before the first frame update
    void Start()
    {
        currentMuzzle.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0,rotAngleSpeed,0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,-rotAngleSpeed,0);
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward*playerSpeed*Time.deltaTime*Mathf.Pow(speedBoost,activeBoost));
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward*playerSpeed*Time.deltaTime);
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            activeBoost=0;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            activeBoost=1;
        }
    }

    void Update()
    {
        if(selectedWeapon==0 && Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(bullet,transform.position+transform.forward,transform.rotation);
            RaycastHit hit;
            Physics.Raycast(transform.position,transform.forward,out hit, 10f);
            StartCoroutine(pistolShot());
            if(hit.collider!=null)
            {
                Debug.Log(hit.collider.name);
                if(hit.collider.GetComponent<EnemyAbs>())
                {
                    hit.collider.GetComponent<EnemyAbs>().enemyHit(selectedWeaponDamage);
                }
                else if(hit.collider.tag=="Wall") 
                {
                    GameObject hole = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    hole.GetComponent<MeshRenderer>().material.color = Color.black;
                    hole.transform.localScale = new Vector3(0.05f,0.05f,0.05f);
                    hole.GetComponent<Collider>().enabled=false;
                    hole.transform.position = hit.point;
                    Destroy(hole,5);
                }
            }
        }
    }

    IEnumerator pistolShot()
    {
        currentMuzzle.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        currentMuzzle.SetActive(false);
    }

    public void hit(int damage)
    {
        health-=damage;
        Debug.Log(health);
    }
}
