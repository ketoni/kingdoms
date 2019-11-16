using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] float hp = 20;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] float explodeForce = 200;
    [SerializeField] float explodeMaxDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            //CreateChunks()
            Explode();
            Destroy(gameObject);
        }
    }
    public void GetHit(float force)
    {
       // Debug.Log("HIT: " + force);
        hp -= force;
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
