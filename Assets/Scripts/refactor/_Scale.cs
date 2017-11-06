using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Scale : MonoBehaviour {

	public Vector3 Step = new Vector3(.5f,.5f,.5f);

	void Update () {
		if (enabled)
		{
			transform.localScale = transform.localScale + Step * Time.deltaTime;
		}
	}
}
