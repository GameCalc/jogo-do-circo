using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    int batteriesCount;
    int maxCharges = 2;

    public float chargeDuration = 15;
    float timeCounter = 0;

    [HideInInspector]
    public bool flashlightOn = false;

    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
	}

    void Update () {
        if (flashlightOn)
        {
            timeCounter += Time.deltaTime;

            if (timeCounter >= chargeDuration)
            {
                if (batteriesCount > 0)
                    batteriesCount--;

                timeCounter = 0;
            }
        }        
    }

    // Tenta coletar a bateria e retonar se conseguiu.
    public bool CollectBattery () {
        if (batteriesCount < maxCharges) {
            batteriesCount++;
            return true;
        }

        return false;
    }

    public int GetBatteriesCount ()
    {
        return batteriesCount;
    }

    // Muda o estado da lanterna, se possível, e retorna o estado após a modificação
    public void UpdateFlashlight ()
    {
        if (batteriesCount > 0 && !flashlightOn)
        {
            flashlightOn = true;
        } else if (flashlightOn)
        {
            flashlightOn = false;
        }
    }
}
