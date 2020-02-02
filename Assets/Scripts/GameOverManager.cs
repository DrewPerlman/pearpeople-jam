using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
	private void Update(){
		if(Input.GetKey(KeyCode.R)){
			RestartStage();
		} else if(Input.GetKey(KeyCode.Escape)){
			ExitGame();
		}
	}

	public void RestartStage(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitGame(){
		//Application.Quit();
		SceneManager.LoadScene(0);
	}
}
