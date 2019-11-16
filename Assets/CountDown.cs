using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] int time = 3;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DecreaseTime", 1f);
    }
    private void DecreaseTime()
    {
        time--;
        GetComponent<Text>().text = time.ToString();
        if (time == 0) GetComponent<Text>().text = "GO!";
        else if (time <= -1)
        {
            FindObjectOfType<GameController>().StartMatch();
            Destroy(gameObject);
        }
        Invoke("DecreaseTime", 1f);
    }
}
