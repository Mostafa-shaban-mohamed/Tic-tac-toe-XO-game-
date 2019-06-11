using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour {

	void Start()
	{
		StartCoroutine (loadSceneAfterDelay(6));
	}

	IEnumerator loadSceneAfterDelay(float waitBySecs)
	{
		yield return new WaitForSeconds (waitBySecs);
		Application.LoadLevel (1);
	}
}
