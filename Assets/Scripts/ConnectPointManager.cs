using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ConnectPointManager : MonoBehaviour
{
	public GameObject[] ConnectPoints;
	public StartCube StartCube;

	public void DisableExcept(GameObject connectPoint)
	{
		ConnectPoints
			.Where(c => c != connectPoint)
			.ForEach(c => c.gameObject.transform.parent.transform.gameObject.SetActive(false));
	}
}
