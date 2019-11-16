using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cannon : NetworkBehaviour
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
    
    // Start is called before the first frame update
    void Start()
    {
        resolution = Screen.currentResolution;
        maxDistance = resolution.width / 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) // allow only ourselves to shoot
        {
            return;
        }
        if (dragging)
        {
            UpdateSize(Input.mousePosition);
        }
        if(Input.GetMouseButtonDown(0))
        {
            dragging = true;
            pressPos = Input.mousePosition;
            CmdSpawnAmmo();
        }
        if(Input.GetMouseButtonUp(0))
        {
            dragging = false;
            Shoot(Input.mousePosition);
        }
    }

    private void UpdateSize(Vector2 currentPos)
    {
        float distance = Vector2.Distance(pressPos, currentPos);

        distance = Mathf.Min((distance / maxDistance) * 10, maxSize);
        Debug.Log("distance " + distance + " " + maxSize);
        scale = (int)distance;
        scale = Mathf.Max(1, scale);
        Vector2 size = new Vector2(scale, scale);
        CmdUpdateSize(size);
    }
    private void Shoot(Vector2 releasePos)
    {
        Vector2 direction = pressPos - releasePos;
        direction = direction.normalized;
        CmdShoot(direction, scale, power);
    }

    [Command]
    private void CmdSpawnAmmo()
    {
        RpcSpawnAmmo();
    }


    [Command]
    private void CmdShoot(Vector2 bulletDirection, int bulletScale, float bulletPower)
    {
        RpcShoot(bulletDirection, bulletScale, bulletPower);
    }

    [Command]
    private void CmdUpdateSize(Vector2 size)
    {
        RpcUpdateSize(size);
    }

    [ClientRpc]
    private void RpcSpawnAmmo()
    {
        ammo = Instantiate(ammoPrefab, transform);
    }

    [ClientRpc]
    private void RpcShoot(Vector2 bulletDirection, int bulletScale, float bulletPower)
    {
        //shoot
        ammo.AddComponent<Rigidbody2D>();
        Rigidbody2D rb = ammo.GetComponent<Rigidbody2D>();
        rb.mass = bulletScale;
        Vector2 force = bulletDirection * rb.mass * bulletPower;
        rb.AddForce(force);
        ammo.GetComponent<Ammo>().Shoot();
    }

    [ClientRpc]
    private void RpcUpdateSize(Vector2 size)
    {
        ammo.transform.localScale = size;
    }
}
