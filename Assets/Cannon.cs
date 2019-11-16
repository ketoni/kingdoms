using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] float power = 200;

    private float maxDistance;
    private bool dragging;
    private Vector2 pressPos;
    private Resolution resolution;
    private float maxSize = 10;
    private int scale;
    private GameObject ammo;
    private GameController controller;

    // Start is called before the first frame update
    void Start()
    {
        resolution = Screen.currentResolution;
        maxDistance = resolution.width / 4;
        controller = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.matchStarted)
        {

            if (dragging)
            {
                UpdateSize(Input.mousePosition);
            }
            if (Input.GetMouseButtonDown(0))
            {
                dragging = true;
                pressPos = Input.mousePosition;
                SpawnAmmo();
            }
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                Shoot(Input.mousePosition);
                FindObjectOfType<ResourceBarController>().UseMana(scale);
            }
        }
    }

    private void UpdateSize(Vector2 currentPos)
    {

        float distance = Vector2.Distance(pressPos, currentPos);

        distance = Mathf.Min((distance / maxDistance) * 10, maxSize);
        scale = (int)distance;
        scale = Mathf.Max(1, scale);
        var resourceBar = FindObjectOfType<ResourceBarController>();
        scale = Mathf.Min(scale, resourceBar.AvailableMana());
        resourceBar.DrawManaUsage(scale);

        Vector2 size = new Vector2(scale, scale);
        ammo.transform.localScale = size;

    }

    private void SpawnAmmo()
    {
        ammo = Instantiate(ammoPrefab, transform);
    }

    private void Shoot(Vector2 releasePos)
    {
        Vector2 direction = pressPos - releasePos;
        direction = direction.normalized;
        //shoot
        ammo.AddComponent<Rigidbody2D>();
        Rigidbody2D rb = ammo.GetComponent<Rigidbody2D>();
        rb.mass = scale;
        Vector2 force = direction * rb.mass * power;
        rb.AddForce(force);
        ammo.GetComponent<Ammo>().Shoot();
    }
}
