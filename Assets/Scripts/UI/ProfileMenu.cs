using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ProfileMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
	[SerializeField] private GameObject profileMenu;
    [SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject loadingScreen;
	[SerializeField] private GameObject confirmTrashPanel;
	[SerializeField] private GameObject confirmNamePanel;
	[SerializeField] private GameObject confirmResetPanel;
	[SerializeField] private GameObject noProfilePanel;
	[SerializeField] private GameObject header;
	[SerializeField] private GameObject profileDetailPanel;
	[SerializeField] private GameObject footer;
	[SerializeField] private StringEventChannelSO sendProfileName;
	[SerializeField] private VoidEventChannelSO displayIDs;
	[SerializeField] private VoidEventChannelSO checkProfileSelection;
	[SerializeField] private IntEventChannelSO setProfilePointer;
	[SerializeField] private VoidEventChannelSO deleteProfile;
	[SerializeField] private VoidEventChannelSO resetProfile;
	[SerializeField] private VoidEventChannelSO startGame;
	[SerializeField] private VoidEventChannelSO triggerNextLevel;
    [SerializeField] private GameObject HUD;
	private EmployeeData previousSession;
	
    private void OnEnable()
    {
        Time.timeScale = 1f;
		displayIDs.RaiseEvent();
		checkProfileSelection.RaiseEvent();
    }

	public void BackButton()
	{
		profileMenu.SetActive(false);
	}
	
    public void LoadGame()
    {
		startMenu.SetActive(false);
        profileMenu.SetActive(false);
		startGame.RaiseEvent();
		triggerNextLevel.RaiseEvent();
    }
	
	public void NameProfile()
    {
		TMP_InputField profileField = confirmNamePanel.GetComponentInChildren<TMP_InputField>();
		profileField.text = "Enter your name here";
		
    }

    public void ConfirmProfileName()
    {
		previousSession = LevelManager.Instance.currentSession;
		TMP_InputField profileField = confirmNamePanel.GetComponentInChildren<TMP_InputField>();
		sendProfileName.RaiseEvent(profileField.text);
		displayProfile(LevelManager.Instance.currentSession.employeeNumber);
		setProfilePointer.RaiseEvent(LevelManager.Instance.currentSession.employeeNumber);
		confirmNamePanel.SetActive(false);
    }

    public void CancelProfileName()
    {
		LevelManager.Instance.currentSession = previousSession;
        confirmNamePanel.SetActive(false);
    }
	
	public void TrashProfile()
	{
		confirmTrashPanel.SetActive(true);
	}
	
	public void CancelTrash()
	{
		confirmTrashPanel.SetActive(false);
	}
	
	public void ConfirmTrash()
	{	
		deleteProfile.RaiseEvent();
		displayProfile(-1);
		setProfilePointer.RaiseEvent(-1);
		confirmTrashPanel.SetActive(false);
	}
	
	public void ResetProfile()
	{
		confirmResetPanel.SetActive(true);
	}
	
	public void CancelReset()
	{
		confirmResetPanel.SetActive(false);
	}
	
	public void ConfirmReset()
	{	
		resetProfile.RaiseEvent();
		displayProfile(LevelManager.Instance.currentSession.employeeNumber);
		setProfilePointer.RaiseEvent(LevelManager.Instance.currentSession.employeeNumber);
		LevelManager.Instance.saveGame();
		confirmResetPanel.SetActive(false);
	}
	
	public void displayProfile(int selection)
	{
		if(selection != -1)
		{
			if(LevelManager.Instance.currentSession.employeeName != "")
			{
				noProfilePanel.SetActive(false);
				Transform planetDay = profileDetailPanel.transform.Find("PlanetDay");
				foreach(Transform child in planetDay)
				{
					if(child.name == "Planet (TMP)")
					{
						TMP_Text planet = child.gameObject.GetComponent<TMP_Text>();
						string displayPlanet = "Planet: ";
						if(LevelManager.Instance.currentSession.currentDay <= 3)	
						{
							displayPlanet = displayPlanet + "Athanor";
						}
						else if(LevelManager.Instance.currentSession.currentDay <= 6)
						{
							displayPlanet = displayPlanet + "Melligo";
						}
						else
						{
							displayPlanet = displayPlanet + "Crasis";
						}
						planet.text = displayPlanet;
					}
					if(child.name == "Day (TMP)")
					{
						TMP_Text day = child.gameObject.GetComponent<TMP_Text>();
						string displayDay = "Day " + LevelManager.Instance.currentSession.currentDay;
						day.text = displayDay;
					}
				}
				Transform hours = profileDetailPanel.transform.Find("Hours");
				foreach(Transform child in hours)
				{
					if(child.name == "Timer (TMP)")
					{
						TMP_Text time = child.gameObject.GetComponent<TMP_Text>();
						//figure out how to do time here
					}
				}
				updateEndings();
				updateAchievements();
				
				header.SetActive(true);
				profileDetailPanel.SetActive(true);
				footer.SetActive(true);
			}
		}
		else
		{
			header.SetActive(false);
			profileDetailPanel.SetActive(false);
			footer.SetActive(false);
			noProfilePanel.SetActive(true);
		}
	}
	
	private void updateEndings()
	{
		//no endings yet
	}
	
	private void updateAchievements()
	{
		//no achievements yet
	}
}
