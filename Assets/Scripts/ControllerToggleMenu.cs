using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_ControllerEvents))]
public class ControllerToggleMenu : MonoBehaviour
{
	public VRTK_ControllerEvents _controllerEvents { get; private set; }
	public GameObject Menu;

	private bool _menuCurrentlyActive = true;

	public void Start()
	{
		this._controllerEvents = GetComponent<VRTK_ControllerEvents>();
		this._controllerEvents.ButtonOnePressed += this.OnButtonOnePressed;
		this._menuCurrentlyActive = this.Menu.activeSelf;
	}

	private void OnButtonOnePressed(object sender, ControllerInteractionEventArgs e)
	{
		this.ToggleMenu();
	}

	public void ToggleMenu()
	{
		this._menuCurrentlyActive = !this._menuCurrentlyActive;
		Menu.SetActive(this._menuCurrentlyActive);
	}
}
