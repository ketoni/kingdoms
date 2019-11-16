using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private bool destroyCalled = false;
    [SerializeField] float lifeTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disappear", lifeTime);
    }

    // Update is called once per frame
    void Disappear()
    {
        Destroy(gameObject);
    }
}
