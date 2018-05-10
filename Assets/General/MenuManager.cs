using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public AudioSource StartSound = null;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		else if (Input.anyKeyDown)
		{
			if (StartSound)
				StartSound.Play();

			SceneManager.LoadScene("level_0");
		}
	}
}
