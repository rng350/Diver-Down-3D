using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
	void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			SceneManager.LoadScene(0);
		}
	}
}