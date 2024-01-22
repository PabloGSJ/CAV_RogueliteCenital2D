using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarsController : MonoBehaviour
{
    // VARIABLES
    public Pillar[] pillars;

    // Fire bullets
    public float Step = 0.1f;       // retraso entre disparos
    // FireInAllDirections()
    public int RadialBullets;
    public int RadialRepeats;


    public void FireInAllDirections()
    {
        StartCoroutine(CFireInAllDirections());
    }
    private IEnumerator CFireInAllDirections()
    {
        foreach (Pillar p in pillars)
        {
            Vector2 direction = Vector2.down;
            float startAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;           // degrees
            float angle;
            Vector2 shootingVector;
            float spread = 360f / RadialBullets;

            for (int r = 0; r < RadialRepeats; r++)
            {
                for (int i = 0; i < RadialBullets; i++)
                {
                    angle = (startAngle + (i - RadialBullets / 2) * spread) * Mathf.Deg2Rad;     // radians
                    shootingVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    p.Shoot(shootingVector);
                }
                yield return new WaitForSeconds(Step);
            }
        }

    }
}
