using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAndHealthRotationScript : MonoBehaviour
{

    private float time;
    private float spriteTimer = 0;

    public Sprite currentSprite;

    public Sprite sprite0;
    public Sprite sprite1; 
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;

    public float timeToChange;

    void Awake()
    {
        currentSprite = sprite0;
        spriteTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSprite == sprite0)
        {
            time = Time.time - spriteTimer;
            if (time >= timeToChange)
            {
                currentSprite = sprite1;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
        }
        else if (currentSprite == sprite1)
        {
            time = Time.time - spriteTimer;
            if (time >= timeToChange)
            {
                currentSprite = sprite2;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
        }
        else if (currentSprite == sprite2)
        {
            time = Time.time - spriteTimer;
            if (time >= timeToChange)
            {
                currentSprite = sprite3;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
        }
        else if (currentSprite == sprite3)
        {
            time = Time.time - spriteTimer;
            if (time >= timeToChange)
            {
                currentSprite = sprite4;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
        }
        else if (currentSprite == sprite4)
        {
            time = Time.time - spriteTimer;
            if (time >= timeToChange)
            {
                currentSprite = sprite5;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
        }
        else if (currentSprite == sprite5)
        {
            time = Time.time - spriteTimer;
            if (time >= timeToChange)
            {
                currentSprite = sprite0;
                spriteTimer = Time.time;
                this.GetComponent<SpriteRenderer>().sprite = currentSprite;
            }
        }
    }
}
