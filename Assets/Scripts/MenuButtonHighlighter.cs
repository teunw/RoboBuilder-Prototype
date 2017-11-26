using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

[RequireComponent(typeof(VRTK_OutlineObjectCopyHighlighter))]
public class MenuButtonHighlighter : VRTK_InteractableObject
{
    public GameObject PrefabToSpawn;
    public Transform NewParent;
    public float NewScale = 1f;
    
    private VRTK_OutlineObjectCopyHighlighter _highlighter;
    private bool _isHighlighted = false;
    private VRTK_InteractUse _usingObject;

    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        this._usingObject = usingObject;
        this._usingObject.UseButtonPressed += this.OnUsingObjectUseButtonPressed;
        ToggleHighlight();
    }

    public override void StopUsing(VRTK_InteractUse usingObject)
    {
        base.StopUsing(usingObject);
        this._usingObject.UseButtonPressed -= this.OnUsingObjectUseButtonPressed;
        this._usingObject = null;
        ToggleHighlight();
    }

    public void OnUsingObjectUseButtonPressed(object sender, ControllerInteractionEventArgs controllerEventArgs)
    {
        SpawnPrefabInUsingObject();
    }

    private void SpawnPrefabInUsingObject()
    {
        var inst = Instantiate(this.PrefabToSpawn);
        inst.transform.parent = this.NewParent;
        inst.transform.position = this.usingObject.gameObject.transform.position;
        inst.transform.localScale = new Vector3(NewScale, NewScale, NewScale);
        this.usingObject.interactGrab.AttemptGrab();
        Destroy(inst.GetComponent<MenuButtonHighlighter>());
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