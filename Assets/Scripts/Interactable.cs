using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
	bool inRange;

	// Update is called once per frame
	void Update () {
		if (inRange && Input.GetButtonDown ("Interact"))
			OnInteract ();
	}

	protected abstract void OnInteract ();

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
			inRange = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
			inRange = false;
	}
}
