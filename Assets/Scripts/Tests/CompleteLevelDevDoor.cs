using UnityEngine;

public class CompleteLevelDevDoor : MonoBehaviour
{

	[SerializeField] private VoidEventChannelSO winLevel;
	[SerializeField] private VoidEventChannelSO loseLevel;
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
        {
            winLevel.RaiseEvent();
        }
		if (Input.GetKeyDown(KeyCode.P))
        {
            loseLevel.RaiseEvent();
        }
	}
}
