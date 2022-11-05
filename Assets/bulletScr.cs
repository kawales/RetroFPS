using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScr : MonoBehaviour
{
    [SerializeField] float bulletSpeed=2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*bulletSpeed);
    }
}
