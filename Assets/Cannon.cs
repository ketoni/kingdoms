using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;

    private float maxDistance;
    private bool dragging;
    private Vector2 pressPos;
    private Resolution resolution;
    // Start is called before the first frame update
    void Start()
    {
        resolution = Screen.currentResolution;
        maxDistance = resolution.width / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragging = true;
            pressPos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            dragging = false;
            Shoot(Input.mousePosition);
        }

    
    }
    private void Shoot(Vector2 releasePos)
    {
        Vector2 direction = pressPos - releasePos;
        direction = direction.normalized;
        float distance = Vector2.Distance(pressPos, releasePos);
        GameObject ammo = Instantiate(ammoPrefab, transform);
        //set size
        float scale = Mathf.Max(distance, 1);
        Vector2 size = new Vector2(scale, scale);
        ammo.transform.localScale = size;
        //set mass
        Rigidbody2D rb = ammo.GetComponent<Rigidbody2D>();
        rb.mass = scale;
        //shoot
        Vector2 force = direction * distance * rb.mass;
        rb.AddForce(force);
    }
}
