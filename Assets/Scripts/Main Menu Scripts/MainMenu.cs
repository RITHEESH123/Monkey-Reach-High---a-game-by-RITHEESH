﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Button").GetComponent<Button>().onClick.AddListener(PlayGame);

    }

    void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
    
}
