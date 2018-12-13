using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {
    bool inRange;
    public bool destroyWhenCollected = true;

    void Update()
    {
        if (inRange && Input.GetButtonDown("Interact") && GameManager.Instance.CollectBattery() && destroyWhenCollected)
            Destroy(gameObject);
    }

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
