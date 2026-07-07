using UnityEngine;

public class CompleteLevelDevDoor : MonoBehaviour
{

	[SerializeField] private VoidEventChannelSO winLevel;
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
        {
            winLevel.RaiseEvent();
        }
	}
}
