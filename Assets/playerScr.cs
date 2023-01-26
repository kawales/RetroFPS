using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class playerScr : MonoBehaviour
{
    [System.Serializable]struct Weapon
    {
        
        public int maxAmmo;
        public int currentAmmo;
        public float shootCoolDown ;
        public int damage;
        public GameObject gunModel;
        public GameObject muzzle;
        
    }
    [Header("Speed stats")]
    [SerializeField] float rotAngleSpeed=1.3f;
    public float playerSpeed=3f;
    public float speedBoost=2f;
    public int activeBoost=0;
    [Header("Weapon stuff")]
    [SerializeField] Weapon pistol;
    [SerializeField] Weapon smg;
    int selectedWeapon=0;
    int selectedWeaponDamage = 1;
    [SerializeField]TMP_Text  ammoUI;
    Weapon[] weapons = new Weapon[2];
    [SerializeField]int health = 5;
    [SerializeField]TMP_Text healthUI;
    IEnumerator shooting;
    // Start is called before the first frame update
    void Start()
    {
        weapons[0] = pistol;
        weapons[1] = smg;
        changeWeapon();  
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

    }

    void Update()
    {
        if(selectedWeapon==0 && Input.GetKeyDown(KeyCode.Space) && weapons[selectedWeapon].currentAmmo>0)
        {
            pistolShot();
        }
        else if(selectedWeapon==1 && Input.GetKeyDown(KeyCode.Space) && weapons[selectedWeapon].currentAmmo>0)
        {
            shooting = smgShot();
            StartCoroutine(shooting);
        }
        else if(Input.GetKeyUp(KeyCode.Space) || weapons[selectedWeapon].currentAmmo<=0)
        {
            StopCoroutine(shooting);
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            activeBoost=0;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            activeBoost=1;
        }
        if(Input.GetKey(KeyCode.Alpha1) && selectedWeapon!=0)
        {
            selectedWeapon=0;
            changeWeapon();
        }
        else if(Input.GetKey(KeyCode.Alpha2) && selectedWeapon!=1)
        {
            selectedWeapon=1;
            changeWeapon();
        }
    }

    IEnumerator muzzleFlash()
    {
        weapons[selectedWeapon].muzzle.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        weapons[selectedWeapon].muzzle.SetActive(false);
    }

    public void hit(int damage)
    {
        health-=damage;
        string tempH="";
        healthUI.text="";
        for(int i=0;i<health;i++)
        {
            healthUI.text+="♥";
        }
        Debug.Log(health);
    }

    void changeWeapon()
    {
        for(int i=0;i<weapons.Length;i++)
        {
            if(i==selectedWeapon)
            {
                weapons[i].gunModel.SetActive(true);
            }
            else 
            {
                weapons[i].gunModel.SetActive(false);
            }
        }
        updateAmmoUI();
    }

    void pistolShot()
    {
            RaycastHit hit;
            Physics.Raycast(transform.position,transform.forward,out hit, 10f);
            StartCoroutine(muzzleFlash());
            weapons[selectedWeapon].currentAmmo--;
            updateAmmoUI();
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

    IEnumerator smgShot()
    {
            RaycastHit hit;
            Physics.Raycast(transform.position,transform.forward,out hit, 10f);
            StartCoroutine(muzzleFlash());
            weapons[selectedWeapon].currentAmmo--;
            updateAmmoUI();
            Debug.Log(weapons[selectedWeapon].currentAmmo);
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
            yield return new WaitForSeconds(weapons[selectedWeapon].shootCoolDown);
            shooting = smgShot();
            StartCoroutine(shooting);
    }

    void updateAmmoUI()
    {
        ammoUI.SetText(weapons[selectedWeapon].currentAmmo+"/"+weapons[selectedWeapon].maxAmmo);
    }

    private void OnTriggerEnter(Collider col)
    {
        string name = col.gameObject.name;
        if(name.Contains("pistolAmmo"))
        {
            weapons[0].currentAmmo+=10;
            if(weapons[0].currentAmmo>weapons[0].maxAmmo)
                weapons[0].currentAmmo=weapons[0].maxAmmo;
        }
        else if(name.Contains("smgAmmo"))
        {
            weapons[1].currentAmmo+=20;
            if(weapons[1].currentAmmo>weapons[1].maxAmmo)
                weapons[1].currentAmmo=weapons[1].maxAmmo;
        }
        if(name.Contains("healthPack"))
        {
            hit(-1);
            if (health>5)
                health=5;
        }
        if(name.Contains("bullet"))
        {
            hit(1);
        }
        updateAmmoUI();
        Destroy(col.gameObject);
    }
}
