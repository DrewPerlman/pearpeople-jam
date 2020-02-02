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
    [SerializeField]
    private Image currentPolaroid;

    private void Awake(){
    	if(instance == null){
    		instance = this;
    	} else{
    		Destroy(this.gameObject);
    	}
    }

    private void Start(){
    	StartCoroutine(SetupBar());
    }

    public void RemoveEntry(GameObject entry){
    	garbageList.Remove(entry);
    	UpdateBar();
    }

    private void UpdateBar(){
    	bar.fillAmount = 1f - (float)garbageList.Count/(float)garbageCount;

    	if(bar.fillAmount >= 1f && Timer.instance != null){
    		Timer.instance.CleanPhoto();
    	}

        Color newAlpha = currentPolaroid.color;
        newAlpha.a = bar.fillAmount;
        currentPolaroid.color = newAlpha;
    }

    private IEnumerator SetupBar(){
        yield return new WaitForSeconds(1f);
        garbageList.AddRange(GameObject.FindGameObjectsWithTag("Garbage"));
        garbageCount = garbageList.Count;
        float amt = 1 - garbageList.Count/garbageCount;
        UpdateBar();
    }
}
