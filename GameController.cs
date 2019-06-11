using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
	public Image Panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColor
{
	public Color PanelColor;
	public Color textColor;
}

public class GameController : MonoBehaviour {

	public Text[] buttonlist;
	private string playerside;
	private string PCside;
	private int value;                                                     //Carries the number of button which PC will choose. 

	public GameObject gameoverpanel;
	public Text gameoverText;


	public GameObject restartButton;

	public float Delay;
	public bool playerMove;

	public Player playerX;
	public Player playerO;
	public PlayerColor activeplayerColor;
	public PlayerColor inactiveplayerColor;
	public GameObject StartInstruction;

	private int moveCount;

	void Awake(){
		restartButton.SetActive (false);
		gameoverpanel.SetActive (false);
		SetGameControllerReferenceOnButtons ();
		moveCount = 0;
		playerMove = true;
	}

	public void Update()
	{
		if (playerMove == false) {
			Delay += Delay * Time.deltaTime;
			if (Delay >= 100) {
				//value = Random.Range (0, 8);
				value = GetPCMove(playerside);
				//if (buttonlist [value].GetComponentInParent<Button> ().interactable == true) {
					buttonlist [value].text = GetPCside ();
					buttonlist [value].GetComponentInParent<Button> ().interactable = false;
					EndTurn ();
				//}
			}
		}
	}

	private int GetPCMove(string side)  //--------------AI Algurithum---------------------------------------------------------------
	{
		for (int i = 0; i < buttonlist.Length; i++) {
			if (buttonlist [i].GetComponentInParent<Button> ().interactable == true) {
				if (i == 0) {
					if (buttonlist [1].text == side && buttonlist [2].text == side || buttonlist [3].text == side && buttonlist [6].text == side || buttonlist [4].text == side && buttonlist [8].text == side) {
						return i;
					} 
				} else if (i == 1) {
					if (buttonlist [0].text == side && buttonlist [2].text == side || buttonlist [4].text == side && buttonlist [7].text == side) {
						return i;
					}
				} else if (i == 2) {
					if (buttonlist [1].text == side && buttonlist [0].text == side || buttonlist [5].text == side && buttonlist [8].text == side || buttonlist [4].text == side && buttonlist [6].text == side) {
						return i;
					}
				} else if (i == 3) {
					if (buttonlist [0].text == side && buttonlist [6].text == side || buttonlist [4].text == side && buttonlist [5].text == side) {
						return i;
					}
				} else if (i == 4) {
					if (buttonlist [1].text == side && buttonlist [7].text == side || buttonlist [3].text == side && buttonlist [5].text == side || buttonlist [0].text == side && buttonlist [8].text == side || buttonlist [2].text == side && buttonlist [6].text == side) {
						return i;
					}
				} else if (i == 5) {
					if (buttonlist [8].text == side && buttonlist [2].text == side || buttonlist [3].text == side && buttonlist [4].text == side) {
						return i;
					}
				} else if (i == 6) { 
					if (buttonlist [0].text == side && buttonlist [3].text == side || buttonlist [7].text == side && buttonlist [8].text == side || buttonlist [4].text == side && buttonlist [2].text == side) {
						return i;
					}
				} else if (i == 7) {
					if (buttonlist [1].text == side && buttonlist [4].text == side || buttonlist [8].text == side && buttonlist [6].text == side) {
						return i;
					} 
				} else if (i == 8) {
					if (buttonlist [5].text == side && buttonlist [2].text == side || buttonlist [7].text == side && buttonlist [6].text == side || buttonlist [4].text == side && buttonlist [0].text == side) {
						return i;
					}
				}
			}
			if (i == 8) {
				while (!buttonlist [i].GetComponentInParent<Button> ().interactable) {
					i--;
				}
				return i;
			}
		}
		return 6;
	}

	void SetGameControllerReferenceOnButtons(){
		for (int i = 0; i < buttonlist.Length; i++) {
			buttonlist [i].GetComponentInParent<GridSpace>().SetGameControllerReference (this);
		}
	}

	public void SetStartingSide(string startingSide)
	{
		playerside = startingSide;
		if (playerside == "X") {
			PCside = "O";
			SetPlayerColors (playerX, playerO);
		} else {
			PCside = "X";
			SetPlayerColors (playerO, playerX);
		}
		StartGame ();
	}

	void StartGame()
	{
		SetBoardInteractable (true);
		SetPlayerButtons (false);
		StartInstruction.SetActive (false);
	}

	public string GetPlayerSide(){
		return playerside;
	}

	public string GetPCside(){
		return PCside;
	}

	public void EndTurn()
	{
		moveCount++;

		for (int i = 0; i < buttonlist.Length; i++)
		{
			if (IsGameWonBy(playerside))
			{
				GameOver(playerside);
			}
			else if (IsGameWonBy(PCside))
			{
				GameOver(PCside);
			}
			else if (moveCount >= 9)
			{
				GameOver("draw");
			}
			else
			{
				ChangeSides();
				Delay = 10;
			}
		}
	}

	private bool IsGameWonBy(string side)
	{
		if (buttonlist[0].text == side && buttonlist[1].text == side && buttonlist[2].text == side)
		{
			return true;
		}
		else if (buttonlist[3].text == side && buttonlist[4].text == side && buttonlist[5].text == side)
		{
			return true;
		}
		else if (buttonlist[6].text == side && buttonlist[7].text == side && buttonlist[8].text == side)
		{
			return true;
		}
		else if (buttonlist[0].text == side && buttonlist[4].text == side && buttonlist[8].text == side)
		{
			return true;
		}
		else if (buttonlist[2].text == side && buttonlist[4].text == side && buttonlist[6].text == side)
		{
			return true;
		}
		else if (buttonlist[0].text == side && buttonlist[3].text == side && buttonlist[6].text == side)
		{
			return true;
		}
		else if (buttonlist[1].text == side && buttonlist[4].text == side && buttonlist[7].text == side)
		{
			return true;
		}
		else if (buttonlist[2].text == side && buttonlist[5].text == side && buttonlist[8].text == side)
		{
			return true;
		}

		return false;
	}

	void SetPlayerColors(Player newplayer, Player oldplayer)
	{
		newplayer.Panel.color = activeplayerColor.PanelColor;
		newplayer.text.color = activeplayerColor.textColor;
		oldplayer.Panel.color = inactiveplayerColor.PanelColor;
		oldplayer.text.color = inactiveplayerColor.textColor;
	}

	void GameOver(string winningPlayer)
	{
		SetBoardInteractable (false);
		restartButton.SetActive (true);
		if (winningPlayer == "draw") {
			SetGameOverText ("It's a Draw!!!");
			SetPlayerColorsInactive ();
		} else {
			SetGameOverText (winningPlayer + " Wins!!");
		}
	}

	void ChangeSides()
	{
		//playerside = (playerside == "X") ? "O" : "X";									//if you want Player vs Player, uncomment this line and comment the below line
		playerMove = (playerMove == true) ? false : true;

		//if (playerside == "X") {														//if you want Player vs Player, uncomment this line and comment the below line
		if (playerMove == true) {
			SetPlayerColors (playerX, playerO);
		} else {
			SetPlayerColors (playerO, playerX);
		}
	}

	void SetGameOverText(string value)
	{
		gameoverpanel.SetActive (true);
		gameoverText.text = value;
	}

	public void RestartGame()
	{
		moveCount = 0;
		gameoverpanel.SetActive (false);
		SetPlayerButtons (true);
		StartInstruction.SetActive (true);
		SetPlayerColorsInactive ();
		playerMove = true;
		Delay = 10;

		for (int i = 0; i < buttonlist.Length; i++) {
			buttonlist [i].text = "";
		}
		restartButton.SetActive (false);
	}

	void SetBoardInteractable(bool toggle)
	{
		for (int i = 0; i < buttonlist.Length; i++) {
			buttonlist [i].GetComponentInParent<Button>().interactable = toggle;
		}
	}

	void SetPlayerButtons(bool toggle)
	{
		playerO.button.interactable = toggle;
		playerX.button.interactable = toggle;
	}

	void SetPlayerColorsInactive()
	{
		playerX.Panel.color = inactiveplayerColor.PanelColor;
		playerX.text.color = inactiveplayerColor.textColor;
		playerO.Panel.color = inactiveplayerColor.PanelColor;
		playerO.text.color = inactiveplayerColor.textColor;
	}

	public void Exit()
	{
		Debug.Log ("The Player Exits");
		Application.Quit ();
	}
}
