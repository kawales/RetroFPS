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
    [Header("Bullet stuff")]
    [SerializeField]GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
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

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Instantiate(bullet,transform.position+transform.forward,transform.rotation);
        }
    }
}
