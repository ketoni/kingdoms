using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject battleGroundPrefab;
    [SerializeField] GameObject countDownPrefab;
    [SerializeField] GameObject endTextPrefab;
    [SerializeField] float resourceFillDelay = 0.5f;
    [SerializeField] float cannonPower = 250f;
    [SerializeField] float lift = 7f;

    private GameObject battleGround;
    private ResourceBarController rbController;
    private GameObject manaBar;
    public bool matchStarted = true;

    private void Start()
    {
        rbController = FindObjectOfType<ResourceBarController>();
        manaBar = FindObjectOfType<ResourceBarController>().gameObject;
        StartMatch();
        //manaBar.SetActive(false);
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        manaBar.SetActive(true);
        Instantiate(battleGroundPrefab);
        Instantiate(countDownPrefab, FindObjectOfType<Canvas>().transform);
    }
    public void StartMatch()
    {
        matchStarted = true;
        Invoke("FillResourceBar", resourceFillDelay);
    }
    private void FillResourceBar()
    {
        
        rbController.RestoreMana(1);
        if (matchStarted)
        {
            Invoke("FillResourceBar", resourceFillDelay);
        }
            
    }
    public void EndGame()
    {
        
        matchStarted = false;
        //Ending animations
        Instantiate(endTextPrefab, FindObjectOfType<Canvas>().transform);
    }
    public void MainMenu()
    {
        Destroy(battleGround);
        mainMenu.SetActive(true);
    }
    public void PigDied()
    {
        //last pig dying
        if(GameObject.FindGameObjectsWithTag("Pig").Length == 1)
        {
            EndGame();
        }
    }

    public float GetCannonPower()
    {
        return cannonPower;
    }
    public float GetLift()
    {
        return lift;
    }
}

























//Mee töihi
