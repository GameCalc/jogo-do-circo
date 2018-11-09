using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    GameObject container;

    private void Start()
    {
        container = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (container.activeSelf)
                Resume();
            else
                Pause();
        }            
	}

    public void LoadMenu ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void Pause ()
    {
        container.SetActive(true);
        Time.timeScale = 0;
    }

    void Resume ()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }
}
