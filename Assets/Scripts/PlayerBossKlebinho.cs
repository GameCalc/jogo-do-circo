using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossKlebinho : Player {
    public GameObject bolinha;
    
	void Update () {
        base.Update();
        if(bolinha != null) {
            if (Input.GetButtonUp(KeyCode.B.ToString())) {
                bolinha.transform.position = this.transform.position;
                bolinha.GetComponent<Rigidbody2D>().AddForce(this.direction);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("BolaBoss")) {
            bolinha = Instantiate(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
