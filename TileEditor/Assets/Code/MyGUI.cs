﻿using UnityEngine;
using System.Collections;

public class MyGUI : MonoBehaviour {

	GridManager gridManager;
	
	Vector2 scrollPosition = new Vector2(0.0f,0.0f);
	// Use this for initialization
	void Start () {
		GameObject spritesObject = GameObject.Find("Background");
		gridManager = spritesObject.GetComponent<GridManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	bool actuallyZeroRed = true;
	bool actuallyZeroGreen = true;
	bool actuallyZeroBlue = true;
	bool actuallyZeroUp = true;
	bool actuallyZeroDown = true;
	bool actuallyZeroLeft = true;
	bool actuallyZeroRight = true;
	bool actuallyZeroTrigger = true;
	bool actuallyZeroAction = true;
	
	void OnGUI() {
		GUISkin skinCopy = (GUISkin)Instantiate(GUI.skin);

		float boxWidthPerc = gridManager.boxWidthPerc;
		float boxWidth = Screen.width*boxWidthPerc;
		float boxX = Screen.width*(1-boxWidthPerc);
		float boxY = 0.0f;
		float boxHeight = Screen.height;
		GUI.Box(new Rect(boxX,boxY,boxWidth,boxHeight),"");
		float scrollContentSize = boxWidth - 16.0f;
		float loadButtonWidth = scrollContentSize * .9f;
		float loadButtonX = Screen.width - boxWidth + scrollContentSize*.05f;
		float loadButtonY = scrollContentSize*.05f;
		float loadButtonHeight = 30.0f;
		float width = GUI.skin.verticalScrollbar.fixedWidth;
		scrollPosition = GUI.BeginScrollView(new Rect(boxX,boxY,boxWidth,boxHeight), scrollPosition, new Rect(boxX,boxY,scrollContentSize,boxHeight*2.0f));
		if (GUI.Button(new Rect(loadButtonX,loadButtonY,loadButtonWidth,loadButtonHeight),"Load File...")) {
			Debug.Log("Button Press");
			/* 			string path = EditorUtility.OpenFilePanel(
				"Overwrite with png",
				"",
				"png");
			Debug.Log(path);
			*/
			gridManager.loadNewBackgroundFile();
		}
		
	//	GUI.enabled = gridManager.imageFileName != null && !gridManager.imageFileName.Equals("");
		float importButtonY = loadButtonY + loadButtonHeight + 5.0f;
		if (GUI.Button(new Rect(loadButtonX,importButtonY,loadButtonWidth,loadButtonHeight),"Import Tile Map")) {
			StartCoroutine(gridManager.importGrid());
		}
	//	GUI.enabled = true;
		float textFieldHeight = 20.0f;
		float longTextLabelW = 15.0f;
		float textLabelX = loadButtonX;
		float textFieldWidth = loadButtonWidth - longTextLabelW;
		float textFieldX = textLabelX + longTextLabelW;
		float redY = importButtonY + loadButtonHeight + 5.0f;
		float greenY = redY + textFieldHeight + 5.0f;
		float blueY = greenY + textFieldHeight + 5.0f;
		float red = gridManager.red;
		float green = gridManager.green;
		float blue = gridManager.blue;
		GUI.Label(new Rect(textLabelX,redY,longTextLabelW,textFieldHeight),"R");
		GUI.Label(new Rect(textLabelX,greenY,longTextLabelW,textFieldHeight),"G");
		GUI.Label(new Rect(textLabelX,blueY,longTextLabelW,textFieldHeight),"B");
		GUI.SetNextControlName("red");string redS = GUI.TextField(new Rect(textFieldX,redY,textFieldWidth,textFieldHeight),(red==0.0f?(actuallyZeroRed||!GUI.GetNameOfFocusedControl().Equals("red")?"0":""):((int)red).ToString()));
		GUI.SetNextControlName("green");string greenS = GUI.TextField(new Rect(textFieldX,greenY,textFieldWidth,textFieldHeight),(green==0.0f?(actuallyZeroGreen||!GUI.GetNameOfFocusedControl().Equals("green")?"0":""):((int)green).ToString()));
		GUI.SetNextControlName("blue");string blueS = GUI.TextField(new Rect(textFieldX,blueY,textFieldWidth,textFieldHeight),(blue==0.0f?(actuallyZeroBlue||!GUI.GetNameOfFocusedControl().Equals("blue")?"0":""):((int)blue).ToString()));
		bool redParsed = float.TryParse(redS,out red);
		bool greenParsed = float.TryParse(greenS,out green);
		bool blueParsed = float.TryParse(blueS,out blue);
//		if (!redParsed) red = 0.0f;
//		if (!greenParsed) green = 0.0f;
//		if (!blueParsed) blue = 0.0f;
		actuallyZeroRed = (red==0.0f && (redS.Length>0 && (redS.ToCharArray()[0]=='0' || redS.ToCharArray()[redS.ToCharArray().Length-1]=='0')));
		actuallyZeroGreen = (green==0.0f && (greenS.Length>0 && (greenS.ToCharArray()[0]=='0' || greenS.ToCharArray()[greenS.ToCharArray().Length-1]=='0')));
		actuallyZeroBlue = (blue==0.0f && (blueS.Length>0 && (blueS.ToCharArray()[0]=='0' || blueS.ToCharArray()[blueS.ToCharArray().Length-1]=='0')));
		red = Mathf.Clamp(red,0.0f,255.0f);
		green = Mathf.Clamp(green,0.0f,255.0f);
		blue = Mathf.Clamp(blue,0.0f,255.0f);
		gridManager.red = red;
		gridManager.green = green;
		gridManager.blue = blue;
		float colorBoxHeight = textFieldHeight;
		Texture2D colorTexture = new Texture2D((int)loadButtonWidth,(int)colorBoxHeight);
		Texture2D oldTexture = GUI.skin.box.normal.background;
		Color fillColor = new Color(red/255.0f,green/255.0f,blue/255.0f,1.0f);
		Color[] colors = colorTexture.GetPixels();
		for (int n=0;n<colors.Length;n++) {
			colors[n] = fillColor;
		}
		colorTexture.SetPixels(colors);
		colorTexture.Apply();
		GUI.skin.box.normal.background = colorTexture;
		float colorBoxY = blueY + textFieldHeight + 5.0f;
		GUI.Box(new Rect(loadButtonX, colorBoxY, loadButtonWidth, colorBoxHeight),"");
		GUI.skin.box.normal.background = oldTexture;
		float checkHeight = colorBoxHeight;
		float checkWidth = loadButtonWidth;
		float checkX = loadButtonX;
		float standableY = colorBoxY + colorBoxHeight + 5.0f;
		float passableY = standableY + checkHeight + 10.0f;
		gridManager.standable = GUI.Toggle(new Rect(checkX,standableY,checkWidth,checkHeight),gridManager.standable,"Can Stand On");
	//	gridManager.passable = GUI.Toggle(new Rect(checkX,passableY,checkWidth,checkHeight),gridManager.passable,"Can Pass Through");
		GUI.Label(new Rect(textLabelX,passableY,loadButtonWidth,textFieldHeight),"Passability:");
		float longTextLabelWidth = 50.0f;
		float longTextFieldWidth = loadButtonWidth - longTextLabelWidth;
		float longTextFieldX = textLabelX + longTextLabelWidth;
		float passableUpY = passableY + textFieldHeight + 5.0f;
		float passableRightY = passableUpY + textFieldHeight + 5.0f;
		float passableDownY = passableRightY + textFieldHeight + 5.0f;
		float passableLeftY = passableDownY + textFieldHeight + 5.0f;
		float triggerY = passableLeftY + textFieldHeight + 15.0f;
		float actionY = triggerY + textFieldHeight + 5.0f;
		GUI.Label(new Rect(textLabelX,passableUpY,longTextLabelWidth,textFieldHeight),"Up");
		GUI.Label(new Rect(textLabelX,passableRightY,longTextLabelWidth,textFieldHeight),"Right");
		GUI.Label(new Rect(textLabelX,passableDownY,longTextLabelWidth,textFieldHeight),"Down");
		GUI.Label(new Rect(textLabelX,passableLeftY,longTextLabelWidth,textFieldHeight),"Left");
		GUI.Label(new Rect(textLabelX,triggerY,longTextLabelWidth,textFieldHeight),"Trigger:");
		GUI.Label(new Rect(textLabelX,actionY,longTextLabelWidth,textFieldHeight),"Action:");
		GUI.SetNextControlName("up");string upS = GUI.TextField(new Rect(longTextFieldX,passableUpY,longTextFieldWidth,textFieldHeight),(gridManager.passableUp==0?(actuallyZeroUp||!GUI.GetNameOfFocusedControl().Equals("up")?"0":""):((int)gridManager.passableUp).ToString()));
		GUI.SetNextControlName("right");string rightS = GUI.TextField(new Rect(longTextFieldX,passableRightY,longTextFieldWidth,textFieldHeight),(gridManager.passableRight==0?(actuallyZeroRight||!GUI.GetNameOfFocusedControl().Equals("right")?"0":""):((int)gridManager.passableRight).ToString()));
		GUI.SetNextControlName("down");string downS = GUI.TextField(new Rect(longTextFieldX,passableDownY,longTextFieldWidth,textFieldHeight),(gridManager.passableDown==0?(actuallyZeroDown||!GUI.GetNameOfFocusedControl().Equals("down")?"0":""):((int)gridManager.passableDown).ToString()));
		GUI.SetNextControlName("left");string leftS = GUI.TextField(new Rect(longTextFieldX,passableLeftY,longTextFieldWidth,textFieldHeight),(gridManager.passableLeft==0?(actuallyZeroLeft||!GUI.GetNameOfFocusedControl().Equals("left")?"0":""):((int)gridManager.passableLeft).ToString()));
		GUI.SetNextControlName("trigger");string triggerS = GUI.TextField(new Rect(longTextFieldX,triggerY,longTextFieldWidth,textFieldHeight),(gridManager.trigger==0?(actuallyZeroTrigger||!GUI.GetNameOfFocusedControl().Equals("trigger")?"0":""):((int)gridManager.trigger).ToString()));
		GUI.SetNextControlName("action");string actionS = GUI.TextField(new Rect(longTextFieldX,actionY,longTextFieldWidth,textFieldHeight),(gridManager.action==0?(actuallyZeroAction||!GUI.GetNameOfFocusedControl().Equals("action")?"0":""):((int)gridManager.action).ToString()));
		bool upParsed = int.TryParse(upS,out gridManager.passableUp);
		bool rightParsed = int.TryParse(rightS,out gridManager.passableRight);
		bool downParsed = int.TryParse(downS,out gridManager.passableDown);
		bool leftParsed = int.TryParse(leftS,out gridManager.passableLeft);
		bool triggerParsed = int.TryParse(triggerS,out gridManager.trigger);
		bool actionParsed = int.TryParse(actionS,out gridManager.action);
		//		if (!upParsed)
		actuallyZeroUp = (gridManager.passableUp==0.0f && (upS.Length>0 && (upS.ToCharArray()[0]=='0' || upS.ToCharArray()[upS.ToCharArray().Length-1]=='0')));
		actuallyZeroRight = (gridManager.passableRight==0.0f && (rightS.Length>0 && (rightS.ToCharArray()[0]=='0' || rightS.ToCharArray()[rightS.ToCharArray().Length-1]=='0')));
		actuallyZeroDown = (gridManager.passableDown==0.0f && (downS.Length>0 && (downS.ToCharArray()[0]=='0' || downS.ToCharArray()[downS.ToCharArray().Length-1]=='0')));
		actuallyZeroLeft = (gridManager.passableLeft==0.0f && (leftS.Length>0 && (leftS.ToCharArray()[0]=='0' || leftS.ToCharArray()[leftS.ToCharArray().Length-1]=='0')));
		actuallyZeroTrigger = (gridManager.trigger==0.0f && (triggerS.Length>0 && (triggerS.ToCharArray()[0]=='0' || triggerS.ToCharArray()[triggerS.ToCharArray().Length-1]=='0')));
		actuallyZeroAction = (gridManager.action==0.0f && (actionS.Length>0 && (actionS.ToCharArray()[0]=='0' || actionS.ToCharArray()[actionS.ToCharArray().Length-1]=='0')));



		float printY = actionY + checkHeight + 15.0f;
		GUI.enabled = gridManager.imageFileName != null && !gridManager.imageFileName.Equals("");
		if (GUI.Button(new Rect(loadButtonX,printY,loadButtonWidth,loadButtonHeight),"Save Tile Map")) {
			gridManager.printGrid();
		}
		GUI.enabled = true;
		GUI.EndScrollView();

		if (gridManager.displayH || gridManager.displayHTime>0) {
			float lW = 200.0f;
			float lH = 100.0f;
			float lX = (Screen.width - lW)/2.0f;
			float lY = (Screen.width - lH)/2.0f;
			GUI.Label(new Rect(lX,lY,lW,lH),"Shift+Control+Alt+" + (gridManager.displayI?"I":"H") + " is pressed!");
		}

		GUI.skin = skinCopy;
	}



}