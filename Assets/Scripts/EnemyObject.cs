using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : Enemy {

	void Update () {
		base.Update();
		if(!cursed){
			this.gameObject.tag = "EnemyObject";
		}else{
			this.gameObject.tag = "Enemy";
		}
	}
}
