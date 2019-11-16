using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] float explodeForce = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetHit(float force)
    {
        Debug.Log("HIT: " + force);
        hp -= force;
        //animate breaking
        if(hp <= 0)
        {
            //CreateChunks()
            Explode();
            Destroy(gameObject);
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
        float range = GetComponent<Collider2D>().bounds.size.x + GetComponent<Collider2D>().bounds.size.y;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach(Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
            AddExplosionForce(rb, explodeForce, transform.position, range);
        }
    }
    public void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
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
