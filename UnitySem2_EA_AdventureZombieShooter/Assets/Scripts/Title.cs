﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit")) {
            int a = 1;
        }
    }

    public void changesceneToGame(){
        DontDestroyOnLoad(GameObject.Find("HighScoreManager"));
        SceneManager.LoadScene("SampleScene");}

    public void changesceneToMenu(){
        DontDestroyOnLoad(GameObject.Find("HighScoreManager"));
        SceneManager.LoadScene("Menu");}
}
