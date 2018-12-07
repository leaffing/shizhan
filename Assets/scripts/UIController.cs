using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] private Text scoreLable;
    [SerializeField] private SettingsPopup settingsPopup;
	// Use this for initialization
	void Start () {
        settingsPopup.Close();
	}
	
	// Update is called once per frame
	void Update () {
        scoreLable.text = Time.realtimeSinceStartup.ToString();
	}

    public void OnOpenSettings()
    {
        //Debug.Log("打开设置");
        settingsPopup.Open();
    }
}
