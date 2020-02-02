using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
	[SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI startText;
    private float timeRemaining;
    private bool photoCleaned;
    private bool started;

    private void Start(){
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

    private void EndStage(bool winState){
    	if(winState){
    			//"You can move on to the next stage"

    		} else{
    			//"You should redo this stage"

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
    }
}
