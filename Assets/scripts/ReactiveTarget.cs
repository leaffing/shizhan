using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReactToHit(){
        WanderingAI behavior = GetComponent<WanderingAI>();
        if(behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    public void CreatOne() //实例化一个对象
    {
        GameObject enemy = Instantiate(this.gameObject);
        enemy.transform.position = new Vector3(0, 1, 0);
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
    }
}
