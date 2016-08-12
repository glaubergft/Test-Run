using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;

public class gameManager : MonoBehaviour {

	private int currentChallenge=0;

	public GameObject panelAboutAndCredits;
	public GameObject panelAtaChallenge;
	public GameObject panelFlash;
	public GameObject panelGameInfo;
	public GameObject panelHint;
	public GameObject panelHome;
	public GameObject panelHowToPlay;
	public GameObject panelGameOver;
	public GameObject panelExitConfirm;

	private Text completedChallenges;
	private Text timer;

	void Start()
	{

		Home();

        //Easter egg. the circular last remaining from 1 to 100 on console:
        gameManager.circularLastRemaining_LIST();

    }

	public void Home()
	{
		
		panelAboutAndCredits.SetActive(false);
		panelAtaChallenge.SetActive(false);
		panelFlash.SetActive(false);
		panelGameInfo.SetActive(false);
		panelHint.SetActive(false);
		panelHome.SetActive(true);
		panelHowToPlay.SetActive(false);
		panelExitConfirm.SetActive(false);
		//panelGameOver.SetActive(false);

		GameObject.Find("Car").transform.position = GameObject.Find("carHomePosition").transform.position;
		GameObject.Find("Car").transform.rotation = GameObject.Find("carHomePosition").transform.rotation;
		GameObject.Find("Car").GetComponent<Rigidbody>().isKinematic = true;

		GameObject.Find("Main_Camera").transform.position = GameObject.Find("cameraHomePosition").transform.position;
		GameObject.Find("Main_Camera").transform.rotation = GameObject.Find("cameraHomePosition").transform.rotation;
	}

	public void newGame()
	{
		panelFlash.SetActive(true);
		iTween.FadeTo(panelFlash, iTween.Hash("alpha", 1, "easetype","linear", "time", 1));

		iTween.ValueTo(panelFlash, iTween.Hash(
			"from", 0,
			"to", 1,
			"time", 1,
			"onupdatetarget", this.gameObject, 
			"onupdate", "fadeInOutFlashPanel"));	

		StartCoroutine(newGame_coroutine(1.1f));
	}

	public IEnumerator newGame_coroutine(float delay)
	{
		yield return new WaitForSeconds(delay);

		iTween.ValueTo(panelFlash, iTween.Hash(
			"from", 1,
			"to", 0,
			"time", 1,
			"onupdatetarget", this.gameObject, 
			"onupdate", "fadeInOutFlashPanel"));	

		panelHome.SetActive(false);
		panelGameInfo.SetActive(true);

		GameObject.Find("timer").GetComponent<Text>().text = "60";
		GameObject.Find("completedChallenges").GetComponent<Text>().text = "0";

		GameObject.Find("Car").transform.position = GameObject.Find("carNewGamePosition").transform.position;
		GameObject.Find("Car").transform.rotation = GameObject.Find("carNewGamePosition").transform.rotation;
		GameObject.Find("Car").GetComponent<Rigidbody>().isKinematic = false;

		GameObject.Find("Main_Camera").transform.position = GameObject.Find("cameraNewGamePostion").transform.position;
		GameObject.Find("Main_Camera").transform.rotation = GameObject.Find("cameraNewGamePostion").transform.rotation;

		InvokeRepeating("reduceTimer", 1f, 1f);

		yield return new WaitForSeconds(1);

		panelFlash.SetActive(false);
	}

	public void fadeInOutFlashPanel(float value)
	{
		panelFlash.GetComponent<Image>().color = new Color(1,1,1,value);
	}

	public void showChallengeAlert(int challengeNumber)
	{
		panelHint.SetActive(true);
		string challengeCode = GameObject.Find("challenge" + challengeNumber.ToString()).GetComponent<challenge>().correctCode;
		string challengeDescription = GameObject.Find("_signs/" + challengeCode).GetComponent<myText>().text;
		panelHint.transform.Find("Text").GetComponent<Text>().text =  challengeDescription;
	}

	public void solveChallenge(Transform side)
	{
		GameObject.Find("timer").GetComponent<Text>().text = (int.Parse(GameObject.Find("timer").GetComponent<Text>().text) +8).ToString();
		GameObject.Find("completedChallenges").GetComponent<Text>().text = (int.Parse(GameObject.Find("completedChallenges").GetComponent<Text>().text) +1).ToString();
		GameObject.Find("correct").GetComponent<AudioSource>().Play();
		panelHint.SetActive(false);
		iTween.RotateAdd(side.gameObject, iTween.Hash("x",360, "islocal",true, "easetype", "easeOutElastic", "time", 5));

		side.parent.Find("left/challengeCollider").GetComponent<Collider>().enabled=false;
		side.parent.Find("right/challengeCollider").GetComponent<Collider>().enabled=false;

		StartCoroutine(resetChallenge(side.parent.GetComponent<challenge>()));
	}

	IEnumerator resetChallenge(challenge challengeItem)
	{
		yield return new WaitForSeconds(10);
		challengeItem.transform.Find("left/challengeCollider").GetComponent<Collider>().enabled=true;
		challengeItem.transform.Find("right/challengeCollider").GetComponent<Collider>().enabled=true;
		challengeItem.setupSigns();
	}

	void reduceTimer()
	{
		GameObject.Find("timer").GetComponent<Text>().text = (int.Parse(GameObject.Find("timer").GetComponent<Text>().text) -1).ToString();

		if (GameObject.Find("timer").GetComponent<Text>().text=="0")
		{
			gameOver();
		}
	}

	public void gameOver()
	{
		panelGameOver.SetActive(true);
		panelGameOver.GetComponent<Image>().color = new Color(1,1,1,0);
		panelGameOver.transform.Find("gameOver").GetComponent<Text>().color = new Color(0,0,0,0);

		GameObject.Find("gameOver").GetComponent<AudioSource>().Play();

		iTween.AudioTo(GameObject.Find("backGroundMusic"), iTween.Hash(
			"volume", 0,
			"time", 2,
			"easetype", "linear"));

		iTween.ValueTo(panelGameOver, iTween.Hash(
			"from", 0,
			"to", 1,
			"time", 2,
			"onupdatetarget", this.gameObject, 
			"onupdate", "fadeInOutGameOverPanel"));	


		CancelInvoke("reduceTimer");

		StartCoroutine(gameOver_coroutine(5.5f));

	}

	public IEnumerator gameOver_coroutine(float delay)
	{
		yield return new WaitForSeconds(delay);

		iTween.ValueTo(panelGameOver, iTween.Hash(
			"from", 1,
			"to", 0,
			"time", 1,
			"onupdatetarget", this.gameObject, 
			"onupdate", "fadeInOutGameOverPanel"));	


		GameObject.Find("backGroundMusic").GetComponent<AudioSource>().Stop();
		GameObject.Find("backGroundMusic").GetComponent<AudioSource>().volume=1;
		GameObject.Find("backGroundMusic").GetComponent<AudioSource>().Play();

		Home();

		yield return new WaitForSeconds(1.01f);
		panelGameOver.SetActive(false);

	}

	public void fadeInOutGameOverPanel(float value)
	{
		
		panelGameOver.GetComponent<Image>().color = new Color(1,1,1,value);
		panelGameOver.transform.Find("gameOver").GetComponent<Text>().color = new Color(0,0,0,value);
	}

	public static int circularLastRemaining(int totalPeople)
	{
        if (totalPeople < 0)
            return 0;

        if (totalPeople<=1)
            return totalPeople; 


        //For a better understanding let's use this graphic for reference:
        //https://dl.dropboxusercontent.com/u/60493728/graph.PNG

        //the blue line represents the sequence of 1 to 100
        //the orange line represents the calculation I've done using circularLastRemaining_NOT_ELEGANT_APPROACH(int)

        //Let's assume totalPeople is equal 23

        //First we convert totalPeople into a binary number
        string totalBin = System.Convert.ToString(totalPeople, 2); 

		//Then we need to find out based on 23 (10111 in binary), when the orange line touched the ground last time
		//10111 leads to 10000

		//Then we do 10111 - 10000
		//So we have 111

		//111 means the jump necessary to achieve totalPeople from the last "grounded" number perspective
		//So we call 111 totalBinLessLastGround:
		string totalBinLessLastGround = totalBin.Substring(1, totalBin.Length - 1);
            
        //Let's convert this into decimal and call it totalLessLastGround
        int totalLessLastGround = System.Convert.ToInt32(totalBinLessLastGround,2);

		//Now we have totalLessLastGround = 7

		//If we double totalLessLastGround and add 1, we finally get the "magic" number
		return totalLessLastGround*2 +1;
	}
	
	
	public static int circularLastRemaining_NOT_ELEGANT_APPROACH(int n)
	{
		List<int> lista = new List<int>();
		for (int i = 1; i <= n; i++)
		{
			lista.Add(i);
		}

		bool saveFirst = true;
		while(lista.Count>1)
		{
			List<int> newList = new List<int>();
			int start = 0;
			if (!saveFirst)
				start = 1;
			for (int i= start; i<=lista.Count-1; i+=2)
			{
				int numero = lista[i];
				newList.Add(numero);
			}
			if (newList[newList.Count - 1] == lista[lista.Count - 1])
				saveFirst = false;
			else
				saveFirst = true;
			lista = newList;
		}
		return lista[0];
	}
	
	public static void circularLastRemaining_LIST()
	{
        StringBuilder sb = new StringBuilder();
		for (int i=1; i<=100; i++)
		{
            sb.AppendLine(circularLastRemaining(i).ToString());
            //sb.AppendLine(circularLastRemaining_NOT_ELEGANT_APPROACH(i).ToString());
		}
        Debug.Log(sb.ToString());
	}

}
