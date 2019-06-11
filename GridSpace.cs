using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

	public Button button;
	public Text buttonText;

	private GameController gamecontroller;

	public void SetSpace(){
		if (gamecontroller.playerMove == true) {
			buttonText.text = gamecontroller.GetPlayerSide ();
			button.interactable = false;
			gamecontroller.EndTurn ();
		}
	}

	public void SetGameControllerReference(GameController controller) {
		gamecontroller = controller;
	}
}
