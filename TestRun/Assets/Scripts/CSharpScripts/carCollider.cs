using UnityEngine;
using System.Collections;

public class carCollider : MonoBehaviour {

	private gameManager manager;

	void Start()
	{
		manager = GameObject.Find("_GameManager").GetComponent<gameManager>();
	}

	// Use this for initialization
	void OnTriggerEnter (Collider c) {		
		challengeAlert challengeAlertItem = c.GetComponent<challengeAlert>();
		if (challengeAlertItem)
			manager.showChallengeAlert(challengeAlertItem.challengeNumber);
		else if(c.name=="challengeCollider")			
			manager.solveChallenge(c.transform.parent);
	}

}
