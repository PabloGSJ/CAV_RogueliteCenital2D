using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimationScript : MonoBehaviour
{

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;

    public Sprite currentSprite;

    public void Awake()
    {
        currentSprite = sprite1;
        this.GetComponent<Button>().image.sprite = currentSprite;
    }
    public void OnMouseEnter()
    {
        currentSprite = sprite2;
        this.GetComponent<Button>().image.sprite = currentSprite;
    }

    public void OnMouseExit()
    {
        currentSprite = sprite1;
        this.GetComponent<Button>().image.sprite = currentSprite;
    }

    public void OnMouseDown()
    {
        currentSprite = sprite3;
        this.GetComponent<Button>().image.sprite = currentSprite;
    }
}
