using UnityEngine;

public class ProfilePointer : MonoBehaviour
{
	private RectTransform rect;
	
	public void Awake()
	{
		if(GetComponent<RectTransform>() != null)
		{
			rect = GetComponent<RectTransform>();
		}
	}
	
	public void setProfilePointer(int position)
	{
		Vector3 pos = rect.localPosition;
		switch(position)
		{
			case 0:
				pos.y = 100;
				rect.localPosition = pos;
				break;
			case 1:
				pos.y = 60;
				rect.localPosition = pos;
				break;
			case 2:
				pos.y = 20;
				rect.localPosition = pos;
				break;
			case 3:
				pos.y = -20;
				rect.localPosition = pos;
				break;	
			case 4:
				pos.y = -60;
				rect.localPosition = pos;
				break;
			case 5:
				pos.y = -100;
				rect.localPosition = pos;
				break;
			case -1:
				//The no profile case
				pos.y = 0;
				rect.localPosition = pos;
				break;
			default:
				Debug.Log("Error selecting profile pointer position");
				break;
		}
	}
}
