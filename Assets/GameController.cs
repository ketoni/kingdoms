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

    private GameObject battleGround;
    private ResourceBarController rbController;
    public bool matchStarted = false;

    private void Start()
    {
        rbController = FindObjectOfType<ResourceBarController>();
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
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
}
