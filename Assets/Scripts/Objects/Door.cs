using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable {
    public bool locked = false;
    public string destinyName;

	protected override void OnInteract () {
		if (!locked)
			SceneManager.LoadScene(destinyName);
	}
}
