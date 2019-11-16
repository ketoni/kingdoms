using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Breakable : NetworkBehaviour
{
    [SerializeField] float hp = 20;
    [SerializeField] GameObject chunkPrefab;
    private float explodeForce = 40;
    [SerializeField] float explodeMaxDamage = 10;
    private GameObject crack;

    // Start is called before the first frame update
    void Start()
    {
        explodeForce = FindObjectOfType<GameController>().GetExplodeForce();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            CmdDie();
        }
        //Check if out of screen and die
    }
    public void GetHit(float force)
    {
        Debug.Log("HIT: " + force);
        hp -= force;
        if(!crack)
        {
            crack = Instantiate(new GameObject());
            SpriteRenderer renderer = crack.AddComponent<SpriteRenderer>();
            int num = Random.Range(1, 2);
            renderer.sprite = Resources.Load<Sprite>("crack"+num);
            renderer.size = GetComponent<SpriteRenderer>().size;
            crack.transform.SetParent(transform);
            renderer.sortingOrder = 1;
            crack.transform.localPosition = Vector3.zero;
            float x = GetComponent<SpriteRenderer>().bounds.size.x*2.7f;
            float y = GetComponent<SpriteRenderer>().bounds.size.y*2.7f;
            renderer.color = new Color(0.7f, 0.7f, 0.7f);
            crack.transform.localScale = new Vector3(x, y, 1);
        }
        
    }

    private void CreateChunks()
    {
        int amount = Random.Range(5, 7);
        for (int i = 0; i < amount; i++)
        {
            GameObject chunk = Instantiate(chunkPrefab);
            chunk.transform.position = transform.position;
            float speed = 100;
            chunk.GetComponent<Rigidbody2D>().AddRelativeForce(Random.onUnitSphere * speed);
        }
    }
    private void Explode()
    {
        float range = (GetComponent<Collider2D>().bounds.size.x + GetComponent<Collider2D>().bounds.size.y)*2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach(Collider2D collider in colliders)
        {
           // Debug.Log("Collider name: "+collider.name);
            if(collider.tag == "Breakable" || collider.tag == "Pig")
            {
                Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
                AddExplosionForce(rb, explodeForce, transform.position, range);
            } 
        }
        //Animate breaking
    }
    public void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        explosionRadius = Mathf.Abs(explosionRadius);
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        wearoff = Mathf.Max(Mathf.Min(wearoff, 1), 0);
        body.AddForce(dir.normalized * explosionForce * wearoff);
        float dmg = ((explosionForce * wearoff)/explosionForce) * explodeMaxDamage;
        body.GetComponent<Breakable>().GetHit(dmg);
    }

    [Command]
    private void CmdDie()
    {
        RpcDie();
    }

    [ClientRpc]
    private void RpcDie()
    {
        //CreateChunks()
        Explode();
        if (tag == "Pig") FindObjectOfType<GameController>().PigDied();
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        CmdDie();
    }

    //public void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    //{
    //    var dir = (body.transform.position - explosionPosition);
    //    float wearoff = 1 - (dir.magnitude / explosionRadius);
    //    Vector3 baseForce = dir.normalized * explosionForce * wearoff;
    //    body.AddForce(baseForce);

    //    float upliftWearoff = 1 - upliftModifier / explosionRadius;
    //    Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
    //    body.AddForce(upliftForce);
    //}
}
