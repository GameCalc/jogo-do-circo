using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
    public bool locked = false;
    public string destinyName;
    bool inRange;

    void Update() {
        if (inRange && Input.GetButtonDown("Interact") && !locked)
            SceneManager.LoadScene(destinyName);
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
