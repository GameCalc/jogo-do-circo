using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movebolateplayer : MonoBehaviour {

    public Vector3 playerPosition;
    private Vector3 alvoDefinitivo;
    private Transform myPosition;
    private bool stopped = false;
    private bool forceAdded = false;

    Vector2 m_NewForce;

    Rigidbody2D m_Rigidbody;
    // Use this for initialization
    void Start () {
        myPosition = this.gameObject.transform;
        alvoDefinitivo = playerPosition;

        m_NewForce = new Vector2(-5.0f, 1.0f);

        m_Rigidbody = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        movimentacao();
	}

    void movimentacao()
    {
        if (this.gameObject.transform.parent == null && !stopped)
        {

            // Se você quiser que fique normal, é só descomentar isso aqui e comentar o codigo restante a baixo
            //transform.position = (Vector2.MoveTowards(myPosition.position, alvoDefinitivo.transform.position, 3 * Time.deltaTime));




            // ATENÇÃO ANDERSON, Se você quiser tirar esse efeito doido que ficou, é só comentar daqui até o a chave que fecha o if, e descomentar o transform acima, mano, desculpa KKKKKKKKKKKK

            //Make the GameObject travel upwards
            if (!forceAdded) {
                m_NewForce = new Vector2((alvoDefinitivo.x - myPosition.position.x) / 2, (alvoDefinitivo.y - myPosition.position.y) / 2);
                //Use Impulse mode as a force on the RigidBody
                m_Rigidbody.AddForce(m_NewForce * 120f, ForceMode2D.Force);
                forceAdded = true;
                Invoke("Destroy", 6f);
            }

            if (Mathf.Abs(myPosition.position.x - alvoDefinitivo.x) <= 0.2f && Mathf.Abs(myPosition.position.y - alvoDefinitivo.y) <= 0.2f)
            {
                stopped = true;
                m_Rigidbody.velocity = new Vector2(0f, 0f);
            }
            
        }
    }

    private void Destroy() {
        Destroy(this.gameObject);
    }
}
