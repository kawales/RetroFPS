using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScr : MonoBehaviour
{
    [SerializeField] float rotAngleSpeed=1f;
    public float playerSpeed=3f;
    public float speedBoost=1.5f;
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
            transform.Translate(Vector3.forward*playerSpeed*Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward*playerSpeed*Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {

        }
    }
}
