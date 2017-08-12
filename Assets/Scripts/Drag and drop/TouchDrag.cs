using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrag : TouchSprite {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		funTouchInput(GetComponent<BoxCollider2D>());
	}

	void funOnFirstTouch() {
		Vector3 pos;
		Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
		pos = new Vector3(clickPos.x, clickPos.y, transform.position.z);
		transform.position = pos;
	}
}
