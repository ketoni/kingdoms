using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarController : MonoBehaviour
{

    public int maxMana = 10;
    public int currentMana = 10;
    public GameObject manaPrefab;
    public GameObject manaGlowPrefab;
    //public GameObject manaBarObject;
    private Canvas overlayCanvas;
    private GameObject manaBar;

    // Start is called before the first frame update
    void Start()
    {
        overlayCanvas = FindObjectOfType<Canvas>();
        manaBar = gameObject;
        //float dist = manaBar.transform.localScale.y / maxMana;
        RectTransform dims = manaBar.GetComponent<RectTransform>();
        float dist = dims.sizeDelta.y / maxMana;

        for (int i = 0; i < maxMana; i++)
        {
            var temp = Instantiate(manaPrefab, manaBar.transform.position + new Vector3(0.0f, dist * i, 0.0f), Quaternion.identity);
            temp.GetComponent<RectTransform>().sizeDelta = new Vector2(dist, dist);
            temp.transform.SetParent(manaBar.transform);
            temp.name = "Mana" + i;
            temp.SetActive(false);
        }
        
    }

    public bool DrawManaUsage(int amount)
    {
        for (int i = 0; i < maxMana; i++)
        {
            manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        }

        for (int i = currentMana-amount; i < currentMana; i++)
        {
            manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(255, 0, 0);
        }
        return false;
        
    }

    public void ClearManaUsage()
    {
        for (int i = 0; i < maxMana; i++)
        {
            manaBar.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(255, 255, 255);
        }
    }

    public bool UseMana(int amount)
    {
        if (amount <= currentMana)
        {
            currentMana -= amount;
            ClearManaUsage();
            DrawMana();
            return true;
        }
        else
            return false;
    }

    public int AvailableMana()
    {
        return currentMana;
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
