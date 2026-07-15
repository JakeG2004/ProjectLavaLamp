using UnityEngine;

public class StartMenu : MonoBehaviour
{
	[SerializeField] private GameObject profileMenu;
	[SerializeField] private GameObject optionsMenu;
	
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
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
