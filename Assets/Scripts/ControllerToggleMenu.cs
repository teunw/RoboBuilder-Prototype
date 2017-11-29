using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

[RequireComponent(typeof(VRTK_ControllerEvents))]
public class ControllerToggleMenu : MonoBehaviour
{
	public VRTK_ControllerEvents _controllerEvents { get; private set; }
	public PalletteManager PalletteManager;

	private bool _menuCurrentlyActive = true;

	public void Start()
	{
		this._controllerEvents = GetComponent<VRTK_ControllerEvents>();
		this._controllerEvents.ButtonOnePressed += this.OnButtonOnePressed;
		this._menuCurrentlyActive = this.PalletteManager.PalletteActive;
	}

	private void OnButtonOnePressed(object sender, ControllerInteractionEventArgs e)
	{
		this.ToggleMenu();
	}

	public void ToggleMenu()
	{
		this._menuCurrentlyActive = !this._menuCurrentlyActive;
		PalletteManager.SetPalletteActive(this._menuCurrentlyActive);
	}
}
