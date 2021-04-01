using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D))))
        {
            rb.velocity = new Vector3();
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(1,0,0) * -speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(1, 0, 0) * speed;
        }
    }
}
