using UnityEngine;

public class GameManager : MonoBehaviour {
    static int batteriesCount;
    static int maxCharges = 2;

    float timeCounter = 0;
    float chargeDuration = 15;

    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= chargeDuration)
        {
            if (batteriesCount > 0)
                batteriesCount--;

            timeCounter = 0;
        }
    }

    public static bool CollectBattery ()
    {
        if (batteriesCount < maxCharges)
        {
            batteriesCount++;

            return true;
        }

        return false;
    }
}
