using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class btnClick : MonoBehaviour {



	public void btnAboutCredits () {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelHome.SetActive(false);
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelAboutAndCredits.SetActive(true);
	}

	public void btnAtaChallenge () {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelHome.SetActive(false);
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelAtaChallenge.SetActive(true);
	}

	public void btnHowToPlay () {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelHome.SetActive(false);
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelHowToPlay.SetActive(true);
	}

	public void btnNewGame () {
		GameObject.Find("_GameManager").GetComponent<gameManager>().newGame();
	}

	public void btnSolveAta()
	{
        string value = GameObject.Find("AtaInputField").GetComponent<InputField>().text;
        if (value!="")
        {
            int lastOne = gameManager.circularLastRemaining(int.Parse(value));
            GameObject.Find("AtaOutputtField").GetComponent<InputField>().text = lastOne.ToString();
        }
        
	}

	public void btnAboutAndCredits_GoBack () {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelHome.SetActive(true);
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelAboutAndCredits.SetActive(false);
	}


	public void btnAtaChallengeGoBack () {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelHome.SetActive(true);
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelAtaChallenge.SetActive(false);
	}
		
	public void btnHowToPlayGoBack() {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelHome.SetActive(true);
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelHowToPlay.SetActive(false);
	}

	public void btnExitGame() {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelExitConfirm.SetActive(true);
	}

	public void btnExitConfirm_no() {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelExitConfirm.SetActive(false);
	}

	public void btnExitConfirm_yes() {
		GameObject.Find("_GameManager").GetComponent<gameManager>().panelExitConfirm.SetActive(false);
		GameObject.Find("_GameManager").GetComponent<gameManager>().gameOver();
	}

}
