using UnityEngine;
using System.Collections;

public class challengeAlert : MonoBehaviour {

	public int challengeNumber;

	// Use this for initialization
	void Start () {
		transform.GetComponent<Renderer>().enabled=false;

		challengeNumber = int.Parse(transform.name.Replace("challenge","").Replace("_alert",""));
	}
	

}
