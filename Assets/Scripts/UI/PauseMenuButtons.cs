using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject currentMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private PauseMenuManager pauseMenuManager;
    [SerializeField] private GameObject confirmStartMenuPanel;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string sceneName = "OfficeWorkplace";

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
        startMenu.SetActive(true);
	}
	
    public void CancelStartMenuLoad()
    {
        confirmStartMenuPanel.SetActive(false);
    }
}
