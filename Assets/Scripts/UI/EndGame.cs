using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGame : MonoBehaviour
{
	private EmployeeData currentSession;
	private GameObject endGameTitleObject;
	[SerializeField] private GameObject[] stamps;
	[SerializeField] private Sprite stampHPC;
	[SerializeField] private Sprite stampCMS;
	[SerializeField] private VoidEventChannelSO triggerMainMenu;
	
	public void OnEnable()
	{
		foreach(Transform child in gameObject.GetComponentsInChildren<Transform>())
		{
			if(child.name == "GameResultText")
			{
				endGameTitleObject = child.gameObject;
			}
		}
		if (LevelManager.Instance != null && LevelManager.Instance.currentSession != null)
        {
            currentSession = LevelManager.Instance.currentSession;
			setEndGameScreen();
        }
	}
	
	public void setEndGameScreen()
	{
		string endTitle = "";
		//Figure out the ending here
		
		setStamps();
		endGameTitleObject.GetComponent<TMP_Text>().text = endTitle;
	}
	
	public void PressMainMenuButton()
	{
		for(int i = 0; i < currentSession.levelBuildChoices.Length; i++)
		{
			currentSession.levelBuildChoices[i] = 0;
		}
		if (LevelManager.Instance != null)
        {
			LevelManager.Instance.saveGame();
		}
		triggerMainMenu.RaiseEvent();
	}
	
	private void setStamps()
	{
		for(int i = 0; i < 9; i++)
		{
			if(currentSession.levelBuildChoices[i] == 1)
			{
				stamps[i].GetComponent<Image>().sprite = stampHPC;
				stamps[i].GetComponent<Image>().color = Color.white;
			}
			else if(currentSession.levelBuildChoices[i] == 2)
			{
				stamps[i].GetComponent<Image>().sprite = stampCMS;
				stamps[i].GetComponent<Image>().color = Color.white;
			}
			else
			{
				stamps[i].GetComponent<Image>().sprite = null;
				stamps[i].GetComponent<Image>().color = Color.clear;
			}
		}
	}
}
