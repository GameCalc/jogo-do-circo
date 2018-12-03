using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Flashlight))]
public class FlashlightEditor : Editor {
    void OnSceneGUI () {
        Flashlight flashlight = (Flashlight)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(flashlight.transform.position, Vector3.back, Vector3.up, 360, flashlight.lightRadius);
        Vector3 viewAngleA = flashlight.DirectionFromAngle(-flashlight.lightAngle / 2, false);
        Vector3 viewAngleB = flashlight.DirectionFromAngle(flashlight.lightAngle / 2, false);

        Handles.DrawLine(flashlight.transform.position, flashlight.transform.position + viewAngleA * flashlight.lightRadius);
        Handles.DrawLine(flashlight.transform.position, flashlight.transform.position + viewAngleB * flashlight.lightRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in flashlight.illuminatedEnemies) {
            Handles.DrawLine(flashlight.transform.position, visibleTarget.position);
        }
    }
}