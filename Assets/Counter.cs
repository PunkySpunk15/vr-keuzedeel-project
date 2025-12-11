using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public float timerCount = 15;
    public TextMeshProUGUI TextElement;
    public List<FireGun> fg = new List<FireGun>();

    private void Update()
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
