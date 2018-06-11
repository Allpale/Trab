using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuUICtrl : MonoBehaviour {

	public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); 
        }
    }