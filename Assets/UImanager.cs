using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject isStartPanel;
    public GameObject settingPanel;
    public GameObject gameOverPanel;
    public static UImanager �nstance;


    private void Awake()
    {
        if (�nstance != null)
        {
            Destroy(�nstance);
        }

        �nstance = this;

    }


    void Start()
    {
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        isStartPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 0;
    }

    void Update()
    {
        
    }

    public void StartBtn() 
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
        isStartPanel.SetActive(true);
    }


    public void SettingBTN(int value) 
    {
        settingPanel.SetActive(true);
        if (value==0)
        {
            Time.timeScale = 0;
        }
        if (value==1)
        {
            Time.timeScale = 1;
        }
        if (value==2)
        {
            Application.Quit();
        }
        if (value==3)
        {
            SceneManager.LoadScene(0);
        }

    }


    public void GameOverBTN() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }



}
