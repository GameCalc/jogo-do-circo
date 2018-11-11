using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBase : MonoBehaviour{
    public string name;
    public string code;
    public string descriptionEffect;
    
    public Texture ImgCard;
    public RawImage RenderImgCard;

	// Use this for initialization
	protected void Start () {

        RenderImgCard.texture = ImgCard;
	}
	
	// Update is called once per frame
	protected void Update () {
	}
}
