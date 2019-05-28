using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Vector2 movement = new Vector2(horizontal, vertical) * speed ;
            transform.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, transform.position.z); ;
        }

    }
}
