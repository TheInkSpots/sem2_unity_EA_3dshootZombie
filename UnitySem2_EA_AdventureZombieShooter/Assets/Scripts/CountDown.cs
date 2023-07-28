using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public float myTimer = 60.0f;
    public Slider slider;
    public GameObject timesup;
    Text text;

    //HighScoreManager temp = new HighScoreManager();
    GameObject HScoreManagerObj;
    HighScoreManager HScoreManager;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();   
        slider.maxValue = myTimer;
        slider.minValue =0;

        HScoreManagerObj = GameObject.Find("HighScoreManager");
        HScoreManager = HScoreManagerObj.GetComponent<HighScoreManager>();

        //var temp = new HighScoreManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (myTimer > 0) {
            myTimer -= Time.deltaTime;
            
        } else {
            myTimer = 0;
            timesup.SetActive(true);
            Time.timeScale = 0;
           //HealthScript.PlayerDied();
            Cursor.lockState = CursorLockMode.None;
            HScoreManager.saveScore();
            DontDestroyOnLoad(HScoreManager);
            
            SceneManager.LoadScene("Menu");
        }
        slider.value = myTimer;
        text.text = "Time:" + (myTimer).ToString("00");
    }

}
