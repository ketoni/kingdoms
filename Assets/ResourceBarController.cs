using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBarController : MonoBehaviour
{

    public int maxMana = 10;
    public int currentMana = 10;
    public GameObject manaPrefab;
    public GameObject manaBarObject;
    private Canvas overlayCanvas;
    private GameObject manaBar;

    // Start is called before the first frame update
    void Start()
    {
        overlayCanvas = FindObjectOfType<Canvas>();
        manaBar = Instantiate(manaBarObject);
        //float dist = manaBar.transform.localScale.y / maxMana;
        for (int i = 0; i < maxMana; i++)
        {
            var temp = Instantiate(manaPrefab, new Vector3(0.0f, 0.8f * i, 0.0f), Quaternion.identity);
            temp.transform.SetParent(manaBar.transform);
            temp.name = "Mana" + i;
            temp.SetActive(false);
        }
        manaBar.transform.SetParent(overlayCanvas.transform);
    }

    public bool DrawManaUsage(int amount)
    {
        if (amount <= currentMana)
        {

            return true;
        }
        else
        {

            return false;
        }
        //Some cool graphics about mana about to be used
    }

    public bool UseMana(int amount)
    {
        if (amount <= currentMana)
        {
            currentMana -= amount;
            DrawMana();
            return true;
        }
        else
            return false;
    }

    public void RestoreMana(int amount)
    {

        currentMana = Mathf.Min(currentMana + amount, maxMana);
        DrawMana();
    }

    void DrawMana()
    {
        for (int i = 0; i < maxMana; i++)
        {
            manaBar.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < currentMana; i++)
        {
            manaBar.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    
    void Update()
    {
        DrawMana();
    }
}
