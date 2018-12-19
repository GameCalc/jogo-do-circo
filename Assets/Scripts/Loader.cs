using UnityEngine;

public class Loader : MonoBehaviour {
    public GameObject gameManager;
    public GameObject audioManager;

    void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);

        
        if (AudioManager.instance == null)
            Instantiate(audioManager);
    }
}
