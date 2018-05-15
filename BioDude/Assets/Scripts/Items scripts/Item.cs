using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public Sprite sprite;
	
	public virtual void Equip (GameObject placeHolder)
    {
        SpriteRenderer spriteRend = placeHolder.GetComponent<SpriteRenderer>();
        spriteRend.sortingOrder = 2;
        spriteRend.sprite = sprite;
    }
}
