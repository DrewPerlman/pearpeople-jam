using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonManager : MonoBehaviour
{
    private void Update(){
    	if(Input.GetKey(KeyCode.Space)){
    		NextStage();
    	}
    }

    public void NextStage(){
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
