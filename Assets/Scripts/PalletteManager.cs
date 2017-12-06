using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletteManager : MonoBehaviour {

	public bool PalletteActive { get; private set; }

	public void SetPalletteActive(bool shouldBeActive)
	{
		this.PalletteActive = shouldBeActive;
	}
}
