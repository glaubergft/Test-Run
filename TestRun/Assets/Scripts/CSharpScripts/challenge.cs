using UnityEngine;
using System.Collections;

public class challenge : MonoBehaviour {

	public string correctCode;

	struct signData
	{
		public int index;
		public string code;
	}

	// Use this for initialization
	void Start () {
		setupSigns();
	}

	public void setupSigns()
	{
		signData leftSign = setupRandomSign("left", -1);
		signData rightSign = setupRandomSign("right", leftSign.index);

		int rnd=Random.Range(1,3);
		if (rnd==1)
		{
			correctCode = leftSign.code;
			transform.Find("left/challengeCollider").GetComponent<Collider>().isTrigger=true;
			transform.Find("right/challengeCollider").GetComponent<Collider>().isTrigger=false;
		}
		else
		{
			correctCode = rightSign.code;
			transform.Find("left/challengeCollider").GetComponent<Collider>().isTrigger=false;
			transform.Find("right/challengeCollider").GetComponent<Collider>().isTrigger=true;
		}
	}

	signData setupRandomSign(string side, int avoidSignIndex)
	{
		int totalSigns = GameObject.Find("_signs").transform.childCount;

		int rnd=Random.Range(0,totalSigns);
		while (rnd==avoidSignIndex)
		{
			rnd=Random.Range(0,totalSigns);
		}
			
		signData sign = new signData();
		sign.index = rnd;
		sign.code = GameObject.Find("_signs").transform.GetChild(rnd).name;
		transform.Find(side+"/sign").GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("signs/"+sign.code);
		transform.Find(side).GetComponent<myText>().text = sign.code;

		if (!transform.Find(side+"/block"))
		{
			Transform challengeColliderTransform = transform.Find(side+"/challengeCollider");
			GameObject block = Instantiate(challengeColliderTransform.gameObject) as GameObject;
			block.name="block";
			block.transform.position = challengeColliderTransform.position;
			block.transform.rotation = challengeColliderTransform.rotation;
			block.transform.parent = challengeColliderTransform.parent;
			block.GetComponent<Collider>().isTrigger=false;
			block.GetComponent<Renderer>().enabled=false;
			if (side=="left")
				block.transform.Translate(-challengeColliderTransform.localScale.x,0,0);
			else
				block.transform.Translate(challengeColliderTransform.localScale.x,0,0);
				
		}

		return sign;
	}

}
