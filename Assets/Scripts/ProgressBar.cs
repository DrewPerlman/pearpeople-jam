using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
	public static ProgressBar instance;

    private List<GameObject> garbageList = new List<GameObject>();
    private int garbageCount;
    [SerializeField]
    private Image bar;

    private void Awake(){
    	if(instance == null){
    		instance = this;
    	} else{
    		Destroy(this.gameObject);
    	}
    }

    private void Start(){
    	garbageList.AddRange(GameObject.FindGameObjectsWithTag("Garbage"));
    	garbageCount = garbageList.Count;
    	float amt = 1 - garbageList.Count/garbageCount;
    	print(amt.ToString());
    	UpdateBar();
    }

    public void RemoveEntry(GameObject entry){
    	garbageList.Remove(entry);
    	UpdateBar();
    }

    private void UpdateBar(){
    	bar.fillAmount = 1f - (float)garbageList.Count/(float)garbageCount;
    }
}
