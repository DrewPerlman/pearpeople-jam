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

    [SerializeField]
    private GameObject controlMenu;

    private int selection = 0;
    private float selectionTimer = 0.1f;
    [SerializeField]
    private List<GameObject> menuItems = new List<GameObject>();
    [SerializeField]
    private List<Button> menuButtons = new List<Button>();

    private void Awake(){
    	UpdateSelection();
    }

    private void Update(){
    	// FillP1(Input.GetKey(KeyCode.Z));
    	// FillP2(Input.GetKey(KeyCode.M));
    	// FillExit(Input.GetKey(KeyCode.Escape));
    	// FillCredits(Input.GetKey(KeyCode.O));
    	
    	// if(StartGame()){
    	// 	SceneManager.LoadScene(LevelToLoad);
    	// }
    	if(selectionTimer > 0f){
    		selectionTimer -= Time.deltaTime;
    	}
    	if(!controlMenu.activeInHierarchy){
	    	if(Input.GetAxis("Vertical") > 0f && selectionTimer <= 0f){
	    		selection -= 1;
	    		if(selection < 0){
	    			selection = menuItems.Count - 1;
	    		}
	    		selectionTimer = 0.3f;
	    		UpdateSelection();
	    	} else if(Input.GetAxis("Vertical") < 0f && selectionTimer <= 0f){
				selection += 1;
				if(selection > menuItems.Count - 1){
	    			selection = 0;
	    		}
	    		selectionTimer = 0.3f;
	    		UpdateSelection();
	    	}
    	}

    	if(Input.GetKeyDown(KeyCode.Space)){
    		if(controlMenu.activeInHierarchy){
    				HideControls();
    			} else{
    				menuButtons[selection].onClick.Invoke();
    			}
    	}
    	if(Input.GetKeyDown(KeyCode.Escape)){
    		if(controlMenu.activeInHierarchy){
    				HideControls();
    			} else{
    				ExitGame();
    			}
    	}
    }

    private void UpdateSelection(){
    	for(int i=0;i<menuItems.Count;i++){
    		if(i == selection){
    				menuItems[i].SetActive(true);
    			} else{
    				menuItems[i].SetActive(false);
    				print(menuItems[i]);
    			}
    	}
    	print(selection);
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

    //changing literally the whole dang thing
    public void NewGame(){
    	SceneManager.LoadScene(LevelToLoad);
    }

    public void ShowControls(){
    	controlMenu.SetActive(true);
    }

    public void HideControls(){
    	controlMenu.SetActive(false);
    }

    public void ExitGame(){
    	Application.Quit();
    }
}
