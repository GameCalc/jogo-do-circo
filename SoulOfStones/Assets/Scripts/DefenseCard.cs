using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseCard : MonoBehaviour {
	[SerializeField]
	private DefenseCardObject cardInfo;
	[SerializeField]
	private RawImage cardImage;
	[SerializeField]
	private Text cost;
	[SerializeField]
	private Text defensePoints;
	[SerializeField]
	private Text cardName;
	[SerializeField] 
	private Text cardEffect;
	void Start () {
		cardImage.texture = cardInfo.cardImage;
		cost.text = cardInfo.cost;
		defensePoints.text = cardInfo.defensePoints;
		cardName.text = cardInfo.cardName; 
		cardEffect.text = cardInfo.cardEffect;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
