using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable {
    public bool destroyWhenCollected = true;

	protected override void OnInteract () {
		if (GameManager.instance.CollectBattery() && destroyWhenCollected)
            Destroy(gameObject);
    }
}
