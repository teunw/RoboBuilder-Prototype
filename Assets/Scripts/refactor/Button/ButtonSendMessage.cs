using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public class ButtonSendMessage : MonoBehaviour
//{
//
//	public string method;
//	
//	private void OnTriggerEnter(Collider other)
//	{
//		var players = GameObject.FindGameObjectsWithTag("Player");
//		if (players.Length > 1)
//		{
//			throw new NotImplementedException();
//		}
//		var player = players.First();
//		if (player == null)
//		{
//			throw new NullReferenceException("No gameobject with Tag \"Player\"");
//		}
//		player.GetComponent<Receiver>().SendMessage(method,SendMessageOptions.RequireReceiver);
//	}
//}
