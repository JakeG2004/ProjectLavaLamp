using UnityEngine;

public class StartMenu : MonoBehaviour
{
	[SerializeField] private GameObject profileMenu;
	[SerializeField] private GameObject optionsMenu;
	[SerializeField] private GameObject confirmQuitPanel;
	
	public void StartGame()
	{
		profileMenu.SetActive(true);
	}
	
	public void LoadOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }
	
	public void QuitGame()
    {
        confirmQuitPanel.SetActive(true);
    }
	
	public void ConfirmQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void CancelQuit()
    {
        confirmQuitPanel.SetActive(false);
    }
}
