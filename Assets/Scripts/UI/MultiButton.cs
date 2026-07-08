using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MultiButton : Button
{
	
	public GameObject[] additionalTargetImages;
	
	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);
		Color targetColor;
		switch(state)
		{
			case SelectionState.Normal:
				targetColor = colors.normalColor;
				break;
			case SelectionState.Highlighted:
				targetColor = colors.highlightedColor;
				break;
			case SelectionState.Pressed:
				targetColor = colors.pressedColor;
				break;
			case SelectionState.Selected:
				targetColor = colors.selectedColor;
				break;
			case SelectionState.Disabled:
				targetColor = colors.disabledColor;
				break;
			default:
				targetColor = Color.white;
				break;
		}
		
		for(int i = 0; i < additionalTargetImages.Length; i++)
		{
			Image targetImage = additionalTargetImages[i].GetComponent<Image>();
			targetImage.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
		}
	}
}
