using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;
using VRTK;

public class MenuGrabbable : VRTK_InteractableObject
{
	[Header("Menu grabbable options", order = -1)]
	public Transform NewParent;
	public float NewScale = 1f;

	private GameObject cloneForLater;
	private bool _hasCopied = false;
	
	public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
	{
		base.OnInteractableObjectGrabbed(e);
		SpawnCloneForLater();
		Debug.Log("Spawning for later");
	}

	public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
	{
		base.OnInteractableObjectUngrabbed(e);
		SetObjectInLevel();
	}

	/// <summary>
	/// Spawns a clone of the current icon for later use
	/// This clone is activated when the grabbed object is put somewhere in the world
	/// </summary>
	public void SpawnCloneForLater()
	{
		if (_hasCopied) return;
		this._hasCopied = true;
		this.cloneForLater = Instantiate(this.gameObject);
		this.cloneForLater.transform.parent = this.transform.parent;
		this.cloneForLater.transform.localScale = this.transform.localScale;
		this.cloneForLater.transform.position = this.transform.position;
		this.cloneForLater.transform.rotation = this.transform.rotation;
		this.cloneForLater.SetActive(false);
	}

	/// <summary>
	/// Places this object in the world and makes it usable
	/// Enlarges the object and removes this component so it doesn't allow it to grabbed anymore
	/// </summary>
	public void SetObjectInLevel()
	{
		this.transform.parent = this.NewParent;
		this.transform.localScale = new Vector3(NewScale, NewScale, NewScale);
		Destroy(this.GetComponent<MenuButtonHighlighter>());
		this.cloneForLater.SetActive(true);
	}
}
