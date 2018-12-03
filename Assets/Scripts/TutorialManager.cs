using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public GameObject[] popups;
    int tutorialStep = 0;
    public Player player;
    public GameObject enemy;
    public GameObject battery;
    public float dialogTime = 2;
    float timeCounter = 0;

	// Use this for initialization
	void Start () {
        player.canMove = false;
        NextStep();
	}
	
	// Update is called once per frame
	void Update () {
        switch (tutorialStep)
        {
            case 1:
                timeCounter += Time.deltaTime;

                if (timeCounter >= dialogTime)
                    NextStep();
                break;

            case 2:
                if (Input.GetAxis("Horizontal") == 1)
                    NextStep();
                break;

            case 3:
                if (player.transform.position.x > 13)
                    NextStep();
                break;

            case 4:
                timeCounter += Time.deltaTime;

                if (timeCounter >= dialogTime)
                    NextStep();
                break;

            case 5:
                if (player.transform.position.x < 2)
                    NextStep();
                break;

            case 6:
                if (GameManager.instance.GetBatteriesCount() > 0)
                    NextStep();
                break;

            case 7:
                if (GameManager.instance.flashlightOn)
                    NextStep();
                break;

            case 8:
                if (player.transform.position.x < -4)
                    NextStep();
                break;

            case 9:
                if (!enemy.GetComponent<Enemy>().cursed)
                    NextStep();
                break;

            case 10:
                if (player.transform.position.x < -13)
                    NextStep();
                break;
        }
	}

    void NextStep ()
    {
        switch (tutorialStep)
        {
            case 0:
                popups[0].SetActive(true);
                break;

            case 1:
                timeCounter = 0;
                player.canMove = true;
                popups[0].SetActive(false);
                popups[1].SetActive(true);                
                break;

            case 2:
                popups[1].SetActive(false);
                break;

            case 3:
                popups[2].SetActive(true);
                player.canMove = false;
                player.GetComponent<Animator>().SetLayerWeight(1, 0);
                break;

            case 4:
                popups[2].SetActive(false);
                player.canMove = true;
                battery.SetActive(true);
                break;

            case 5:
                popups[3].SetActive(true);
                break;

            case 6:
                popups[3].SetActive(false);
                popups[4].SetActive(true);
                break;

            case 7:
                popups[4].SetActive(false);                
                break;

            case 8:
                enemy.SetActive(true);
                popups[5].SetActive(true);
                break;

            case 9:
                popups[5].SetActive(false);
                break;

            case 10:
                popups[6].SetActive(true);
                break;
        }

        tutorialStep++;
    }
}
