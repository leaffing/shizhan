using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private GameObject enemyPerfab;
    [SerializeField] private GameObject pauseMenu;

    private GameObject _enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_enemy == null)
        {
            _enemy = Instantiate(enemyPerfab) as GameObject;
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }

        if(Input.GetKeyDown(KeyCode.Escape))//当按下esc键时显示相应的菜单
        {
            Instantiate(pauseMenu);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true ;
        }
    }
}
