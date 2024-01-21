using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHeartsScript : MonoBehaviour
{
    private int _maxHeartAmount = 5;
    public int StartHearts = 3;
    public int CurHealth;
    private int _maxHealth;
    private int _healthPerHeart = 2;

    // 
    public Image[] healthImages;

    //
    public Sprite[] healthSprites;
    private const int Empty = 2;
    private const int Half = 1;
    private const int Full = 0;

    // Start is called before the first frame update
    void Start()
    {
        CurHealth = StartHearts * _healthPerHeart;
        _maxHealth = _maxHeartAmount * _healthPerHeart;
        CheckHealthAmount();
    }

    private void CheckHealthAmount()
    {
        for(int i = 0; i < _maxHeartAmount; i++)
        {
            if (StartHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
    }

    private void UpdateHearts()
    {
        bool empty = false;
        int i = 0;

        foreach(Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[Empty];
            }
            else
            {
                i++;
                if (CurHealth >= i * _healthPerHeart)
                {
                    image.sprite = healthSprites[Full];
                }
                else
                {
                    image.sprite = healthSprites[Half];
                }
            }
        }
    }
}
