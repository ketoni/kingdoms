using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndText : MonoBehaviour
{
    [SerializeField] int lifeTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", lifeTime);
    }

    private void Die()
    {
        FindObjectOfType<GameController>().MainMenu();
        Destroy(gameObject);
    }
}
