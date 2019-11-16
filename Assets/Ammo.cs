using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] float lift = 0.01f;
    [SerializeField] float destroyDelay = 1f;

    private bool destroyCalled = false;
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

    private void Disappear()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Ammo" && !destroyCalled)
        {
            destroyCalled = true;
            Invoke("Disappear", destroyDelay);
        }
        if(collision.collider.tag == "Breakable")
        {
            collision.collider.GetComponent<Breakable>().GetHit(rb.mass);
        }
    }
}
