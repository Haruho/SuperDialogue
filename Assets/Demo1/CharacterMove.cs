using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {
    public float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(Time.deltaTime * -speed ,0,0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
        }
    }
}
