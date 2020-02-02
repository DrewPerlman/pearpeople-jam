using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
	public static Timer instance;


	[SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI startText;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject loseScreen;
    [SerializeField]
    private GameObject thanksScreen;
    private float timeRemaining;
    private bool photoCleaned;
    private bool started;

    private void Awake(){
    	if(instance == null){
    		instance = this;
    	} else{
    		Destroy(this.gameObject);
    	}
    }

    private void Start(){
    	thanksScreen.SetActive(false);
    	winScreen.SetActive(false);
    	loseScreen.SetActive(false);
    	Initialize();
    }

    private void Initialize(){
    	timeRemaining = 3f;
    	timerText.text = "02:00";
    	photoCleaned = false;
    }

    private void StartGame(){
    	timeRemaining = 120f;
    	started = true;
    	startText.gameObject.SetActive(false);
    }

    private void Update(){
    	if(!photoCleaned){
	    	if(timeRemaining > 0f && !started){
	    		timeRemaining -= Time.deltaTime;
	    		startText.text = Mathf.Ceil(timeRemaining).ToString();
	    	} else if(timeRemaining <= 0f && !started){
	    		StartGame();
	    	}

	    	if(timeRemaining > 0f && started){
	    		timeRemaining -= Time.deltaTime;
	    		DisplayTime();
	    	} else if(timeRemaining <= 0f && started){
	    		EndStage(photoCleaned);
	    		timeRemaining = 0f;
	    		DisplayTime();
	    	}
    	}
    }

    private void EndStage(bool winState){
    	if(winState){
    			//"You can move on to the next stage"
    		if(SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCountInBuildSettings){
    			thanksScreen.SetActive(true);
    		} else{
    			winScreen.SetActive(true);
    		}
    	} else{
    			//"You should redo this stage"
    			loseScreen.SetActive(true);
    		}
    }

    private void DisplayTime(){
    	int secondsRemaining = (int)timeRemaining;
    	int minutesRemaining = 0;
    	while(secondsRemaining > 60){
    		secondsRemaining -= 60;
    		minutesRemaining += 1;
    	}
    	string textToDisplay = (minutesRemaining.ToString().PadLeft(2,'0') + ":" + secondsRemaining.ToString().PadLeft(2,'0'));
    	timerText.text = textToDisplay;
    }

    public void CleanPhoto(){
    	photoCleaned = true;
		FindObjectOfType<AudioManager>().PlayWin();
    	EndStage(photoCleaned);
    }
}
