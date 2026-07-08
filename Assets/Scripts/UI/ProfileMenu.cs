using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ProfileMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
	[SerializeField] private GameObject profileMenu;
    [SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject confirmTrashPanel;
	[SerializeField] private GameObject confirmNamePanel;
	[SerializeField] private StringEventChannelSO sendProfileName;
	[SerializeField] private VoidEventChannelSO displayIDs;
	[SerializeField] private VoidEventChannelSO deleteProfile;
	[SerializeField] private VoidEventChannelSO startGame;
    [SerializeField] private GameObject HUD;
    [SerializeField] private string sceneName = "OfficeWorkplace";
	
    private void OnEnable()
    {
        Time.timeScale = 1f;
		displayIDs.RaiseEvent();
    }

	public void BackButton()
	{
		profileMenu.SetActive(false);
	}
	
    public void LoadGame()
    {
		startGame.RaiseEvent();
		startMenu.SetActive(false);
        profileMenu.SetActive(false);
        HUD.SetActive(true);
		SceneLoader.Instance.LoadScene(sceneName);
        InputSystem.actions.FindActionMap("Player").Enable();
    }
	
	public void NameProfile()
    {
		TMP_InputField profileField = confirmNamePanel.GetComponentInChildren<TMP_InputField>();
		profileField.text = "Enter your name here";
		
    }

    public void ConfirmProfileName()
    {
		TMP_InputField profileField = confirmNamePanel.GetComponentInChildren<TMP_InputField>();
		sendProfileName.RaiseEvent(profileField.text);
		confirmNamePanel.SetActive(false);
    }

    public void CancelProfileName()
    {
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
		confirmTrashPanel.SetActive(false);
	}
}
