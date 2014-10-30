﻿using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	public MapGenerator mapGenerator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Rect moveButtonRect() {
		float moveWidth = 90.0f;
		float moveHeight = 40.0f;
		float moveX = 10.0f;
		float moveY = Screen.height - moveHeight - 10.0f;
		return new Rect(moveX, moveY, moveWidth, moveHeight);
	}

	public bool mouseIsOnGUI() {
		Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		if (mapGenerator) {
			if (mapGenerator.selectedPlayer!=null) {
				if (mapGenerator.lastPlayerPath.Count >1) {
					if (moveButtonRect().Contains(mousePos)) {
						return true;
					}

				}
			}
		}
		return false;
	}

	void OnGUI() {
	//	Debug.Log("OnGUI");
		if (mapGenerator == null) return;


		if (mapGenerator.selectedPlayer!=null) {
			if (mapGenerator.lastPlayerPath.Count >1) {
				if(GUI.Button(moveButtonRect(), "Move")) {
					Debug.Log("Move Player!");
					Player p = mapGenerator.selectedPlayer.GetComponent<Player>();
					p.moving = !p.moving;
//					p.rotating = true;
					p.setRotatingPath();
					p.attacking = true;
				}
			}
		}
	//	Debug.Log("OnGUIEnd");
	}

}