using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class EndGame : MonoBehaviour
{
	private EmployeeData currentSession;
	private GameObject endGameTitleObject;
	[SerializeField] private GameObject[] stamps;
	[SerializeField] private Sprite stampHPC;
	[SerializeField] private Sprite stampCMS;
	[SerializeField] private VoidEventChannelSO triggerMainMenu;
	private int[] ScienceEnding;
	private int[] BadScienceEnding;
	private int[] HPCEnding;
	private int[] CMSEnding;
	private int givenEnding; //0 neutral 1 correct science 2 incorrect science 3 hpc 4 cms 
	
	public void OnEnable()
	{
		ScienceEnding = new int[] {1, 2, 2, 2, 1, 2, 1, 2, 2};
		BadScienceEnding = new int[] {1, 1, 1, 1, 2, 1, 2, 1, 1};
		HPCEnding = new int[] {1, 1, 1, 1, 1, 1, 1, 1, 1};
		CMSEnding = new int[] {1, 2, 2, 2, 2, 2, 2, 2, 2};
		givenEnding = 0;
		
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
		string endTitle = "Quiet Quitting";
		Color endColor = new Color(186f/255f, 189f/255f, 189f/255f, 1f);
		if(ArrayUtility.ArrayEquals(currentSession.levelBuildChoices, ScienceEnding) == true)
		{
			givenEnding = 1;
			endTitle = "Science Spectacular";
			endColor = new Color(47f/255f, 213f/255f, 205f/255f, 1f);
		}
		else if(ArrayUtility.ArrayEquals(currentSession.levelBuildChoices, BadScienceEnding) == true)
		{
			givenEnding = 2;
			endTitle = "Antiscience Anti-Superstar";
			endColor = new Color(1f, 135f/255f, 65f/255f, 1f);
		}
		else if(ArrayUtility.ArrayEquals(currentSession.levelBuildChoices, HPCEnding) == true)
		{
			givenEnding = 3;
			endTitle = "HPC Superstar";
			endColor = new Color(1f, 0f, 0f, 1f);
		}
		else if(ArrayUtility.ArrayEquals(currentSession.levelBuildChoices, CMSEnding) == true)
		{
			givenEnding = 4;
			endTitle = "CMS Believer";
			endColor = new Color(240f/255f, 212f/255f, 57f/255f, 1f);
		}
		setStamps();
		endGameTitleObject.GetComponent<TMP_Text>().text = endTitle;
		endGameTitleObject.GetComponent<TMP_Text>().color = endColor;
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
