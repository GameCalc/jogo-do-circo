using UnityEngine;

public class PlayerTutorial : Player {
    [SerializeField]
    private GameObject tutorialManager;

    private bool pertoDaLanterna = false;

    // Update is called once per frame
    protected void Update() {
        // Toda vez a luz é apagada e reacendida, uma vez que são 4 luzes no ambiente e o jogo precisa saber qual delas acender
        if (flashLightOn) {
            TurnOffFlashlight();
            TurnOnFlashlight();
        }
        else {
            TurnOffFlashlight();
        }
        GetInput();
        // Verifica qual das luzes está ligada e traça um raio a partir dela, através da função VerifyHit
        if (flashlightDown.activeSelf) {
            VerifyHit(Vector2.down, transform);
        }
        else if (flashlightUp.activeSelf) {
            VerifyHit(Vector2.up, transform);
        }
        else if (flashlightLeft.activeSelf) {
            VerifyHit(Vector2.left, transform);
        }
        else if (flashlightRight.activeSelf) {
            VerifyHit(Vector2.right, transform);
        }
    }

    private void GetInput () {
        direction = Vector2.zero;

        if (Input.GetKeyDown(turnLight) && !turnOnLightPressed && tutorialManager.GetComponent<TutorialManager>().PegouLanterna()) {
            turnOnLightPressed = true;
            if (flashLightOn)
                TurnOffFlashlight();
            else
                TurnOnFlashlight();
        }

        if (Input.GetKey(actionButton) && pertoDaLanterna) {
            tutorialManager.GetComponent<TutorialManager>().PegarLanterna();
        }

        if (Input.GetKeyUp(turnLight)) {
            turnOnLightPressed = false;
        }

        if (Input.GetKey(KeyCode.W)) {
            direction += Vector2.up;
            moving = 2;
        }

        if (Input.GetKey(KeyCode.A)) {
            direction += Vector2.left;
            moving = 3;
        }

        if (Input.GetKey(KeyCode.S)) {
            direction += Vector2.down;
            moving = 0;
        }

        if (Input.GetKey(KeyCode.D)) {
            direction += Vector2.right;
            moving = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Portao") {
            tutorialManager.GetComponent<TutorialManager>().AtivarTextoPortao();
        }else if (collision.gameObject.tag == "Lanterna") {
            tutorialManager.GetComponent<TutorialManager>().ChegouPertoLanterna();
            pertoDaLanterna = true;
        }else if (collision.gameObject.tag == "Entrada" && tutorialManager.GetComponent<TutorialManager>().PegouLanterna()) {
            GameManager.Instance.SairDoTutorial();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Lanterna") {
            pertoDaLanterna = false;
        }
    }
}
