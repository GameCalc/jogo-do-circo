using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class lifeBase : MonoBehaviour {

    public int totalLife;
    private int currentLife;

    // Use this for initialization
    protected void Start () {
        currentLife = totalLife;
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}
    public void ApplyDamage(int damage) {
        currentLife -= damage;
        OnDamage();
        if (currentLife <= 0) {
            OnDie();
        }
    }
    public abstract void OnDamage();
    public abstract void OnDie();

}
