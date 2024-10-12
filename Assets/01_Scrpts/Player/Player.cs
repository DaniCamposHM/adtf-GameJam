using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movimiento")]
    public float speed = 7f;
    
    [Header("Caracteriticas")]

    public float hp = 100f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
    }

    void Rotation()
    {
        Vector3 dir = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0);
        float angleY = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = Quaternion.Euler(0, -angleY, 0);
    }

        void Movement()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = dir * speed + new Vector3(0, rb.velocity.y, 0);
        //anim.SetFloat("Speed", Mathf.Atan2(dir.x, dir.z));
    }


}
