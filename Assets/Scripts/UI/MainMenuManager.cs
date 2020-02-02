using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string LevelToLoad;
    [SerializeField]
    private Image P1Icon;
    [SerializeField]
    private Image P2Icon;
    [SerializeField]
    private Image ExitIcon;
    [SerializeField]
    private Image CreditsIcon;

    [SerializeField]
    private GameObject gameMenu;
    [SerializeField]
    private GameObject creditsMenu;

    private void Update(){
    	FillP1(Input.GetKey(KeyCode.Z));
    	FillP2(Input.GetKey(KeyCode.M));
    	FillExit(Input.GetKey(KeyCode.Escape));
    	FillCredits(Input.GetKey(KeyCode.O));
    	
    	if(StartGame()){
    		SceneManager.LoadScene(LevelToLoad);
    	}
    }

    private void FillP1(bool keydown){
    	if(keydown && P1Icon.fillAmount < 1.0f){
    			P1Icon.fillAmount += Time.deltaTime / 2f;
    		} else if(!keydown){
    			P1Icon.fillAmount -= Time.deltaTime;
    		}
    }

    private void FillP2(bool keydown){
    	if(keydown && P2Icon.fillAmount < 1.0f){
    			P2Icon.fillAmount += Time.deltaTime / 2f;
    		} else if(!keydown){
    			P2Icon.fillAmount -= Time.deltaTime;
    		}
    }

    private void FillExit(bool keydown){
    	if(keydown && ExitIcon.fillAmount < 1.0f){
    			ExitIcon.fillAmount += Time.deltaTime / 2f;
    		} else if(!keydown){
    			ExitIcon.fillAmount -= Time.deltaTime;
    		}

    	if(ExitIcon.fillAmount >= 1.0f){
    		if(gameMenu.activeInHierarchy){
    			Application.Quit();
    		} else{
    			creditsMenu.SetActive(false);
    			ExitIcon.fillAmount = 0.0f;
    		}
    	}
    }

    private void FillCredits(bool keydown){
    	if(keydown && CreditsIcon.fillAmount < 1.0f && !creditsMenu.activeInHierarchy){
    			CreditsIcon.fillAmount += Time.deltaTime / 2f;
    		} else if(!keydown){
    			CreditsIcon.fillAmount -= Time.deltaTime;
    		}

    	if(CreditsIcon.fillAmount >= 1.0f){
    		creditsMenu.SetActive(true);
    		CreditsIcon.fillAmount = 0.0f;
    		ExitIcon.fillAmount = 0.0f;
    	}
    }

    private bool StartGame(){
    	if(P1Icon.fillAmount >= 1.0f && P2Icon.fillAmount >= 1.0f){
    		return true;
    	} else{
    		return false;
    	}
    }
}
