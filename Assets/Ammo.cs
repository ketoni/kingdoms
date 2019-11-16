using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] float lift = 0.01f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rb)
        {
            rb.AddForce(new Vector2(0, lift*rb.mass));
        }
    }

    public void Shoot()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
