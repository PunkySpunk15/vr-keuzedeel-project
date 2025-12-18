using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public float timerCount = 6;
    public TextMeshProUGUI TextElement;
    public List<FireGun> fg = new();
    public Duel duel = new();

    private void Update()
    {
        if (duel.active)
        {
            if (timerCount <= 0)
            {
                foreach (FireGun gun in fg)
                {
                    gun.allowFire = true;
                }
            }
            else
            {
                timerCount -= Time.deltaTime;
                TextElement.text = ((uint)timerCount).ToString();
            }
        }
    }
}
