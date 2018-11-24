﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingScreen;

	public void LoadScene(int level)
	{
		loadingScreen.SetActive(true);
		SceneManager.LoadScene(level);		
	}
}
