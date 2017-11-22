using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

[RequireComponent(typeof(VRTK_OutlineObjectCopyHighlighter))]
public class MenuButtonHighlighter : VRTK_InteractableObject
{
	private VRTK_OutlineObjectCopyHighlighter _highlighter;
	private bool _isHighlighted = false;
	
	public override void StartUsing(VRTK_InteractUse usingObject)
	{
		base.StartUsing(usingObject);
		ToggleHighlight();
	}

	public override void StopUsing(VRTK_InteractUse usingObject)
	{
		base.StopUsing(usingObject);
		ToggleHighlight();
	}

	public void ToggleHighlight()
	{
		if (_highlighter == null) this._highlighter = GetComponent<VRTK_OutlineObjectCopyHighlighter>();
		
		this._isHighlighted = !this._isHighlighted;
		if (this._isHighlighted)
		{
			this._highlighter.Highlight(base.touchHighlightColor);
		}
		else
		{
			this._highlighter.Unhighlight();
		}
	}
}
