using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 PlayerToFocus;
    public float speed = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Abfrage
    void Update()
    {
        
    }

    //Umsetzung
    void FixedUpdate()
    {



        PlayerToFocus = GetComponent<PlayerCameraFocus>().PlayerToFocus;

        Vector3.Cross(Vector3.up, new Vector3(PlayerToFocus.x, transform.position.y, PlayerToFocus.z));

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector3.Normalize(Vector3.Cross(Vector3.up, new Vector3(PlayerToFocus.x, transform.position.y, PlayerToFocus.z))) * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector3.Normalize(Vector3.Cross(Vector3.up, new Vector3(PlayerToFocus.x, transform.position.y, PlayerToFocus.z))) * -speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector3.Normalize(new Vector3(PlayerToFocus.x, 0, PlayerToFocus.z)) * -speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = Vector3.Normalize(new Vector3(PlayerToFocus.x, 0, PlayerToFocus.z)) * speed;
        }
        if (!((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S))))
        {
            rb.velocity = new Vector3();
        }
    }

    public Vector2 MoveWithTarget(Vector2 dir,float C, float B) //C = Länge von PlayerToFocus auf einer Höhe, B = länge des Velocity Vector
    {
        


        return (Vector2.down);
    }
}
