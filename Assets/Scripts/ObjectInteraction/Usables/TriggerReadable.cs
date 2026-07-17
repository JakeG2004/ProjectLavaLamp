using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerReadable : MonoBehaviour, IUsable
{
	[SerializeField] private string readableTag;
	private GameObject readable;
	private bool currentlyReading;
	private GameObject HUD;
	
	public void Awake()
	{
		currentlyReading = false;
		HUD = GameObject.Find("HUD");
		Transform[] objects = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
		for (int i = 0; i < objects.Length; i++)
		{
			if (objects[i].tag == readableTag)
			{
				readable = objects[i].gameObject;
			}
		}
	}
	public void UseItem()
	{
		if(currentlyReading == false)
		{
			HUD.SetActive(false);
			InputSystem.actions.FindActionMap("Player").Disable();
			InputSystem.actions.FindAction("UseItem").Enable();
			readable.SetActive(true);
			currentlyReading = true;
		}
		else
		{
			readable.SetActive(false);
			InputSystem.actions.FindActionMap("Player").Enable();
			HUD.SetActive(true);
			currentlyReading = false;
		}
	}
}
