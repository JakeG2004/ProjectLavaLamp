using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject currentMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject startMenu;
	[SerializeField] private GameObject menuButtons;
	[SerializeField] private GameObject menuLogo;
    [SerializeField] private PauseMenuManager pauseMenuManager;
    [SerializeField] private GameObject confirmStartMenuPanel;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string sceneName = "OfficeWorkplace";
	[SerializeField] private VoidEventChannelSO stopProfileTimer;
    public void UnpauseGame()
    {
        pauseMenuManager.WasUnpaused = true;
        currentMenu.SetActive(false);

        // Stops the selection animation of the resume button.
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void LoadOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    public void LoadStartMenu()
    {
        confirmStartMenuPanel.SetActive(true);
    }

    public void ConfirmStartMenuLoad()
    {
        confirmStartMenuPanel.SetActive(false);
        loadingScreen.SetActive(true);
		StartCoroutine(WaitToUnloadScene());
		
    }

	private IEnumerator WaitToUnloadScene(){
		SceneLoader.Instance.UnloadScene(sceneName);
        yield return null;
        UnpauseGame();
		Animator buttonAnimator = menuButtons.GetComponent<Animator>();
		Animator logoAnimator = menuLogo.GetComponent<Animator>();
		Time.timeScale = 1f;
        startMenu.SetActive(true);
		buttonAnimator.SetTrigger("Return");
		logoAnimator.SetTrigger("Return");
		stopProfileTimer.RaiseEvent();
	}
	
    public void CancelStartMenuLoad()
    {
        confirmStartMenuPanel.SetActive(false);
    }
}
