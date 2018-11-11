using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="DefenseCard", menuName="New Defense Card")]
public class DefenseCardObject : ScriptableObject {
	public Texture cardImage;
	public string cost;
	public string defensePoints;
	public string cardName;
	public string cardEffect;
}
