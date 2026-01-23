using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public float timerCount = 6;
    public TextMeshProUGUI TextElement;
    public List<FireGun> fg = new();
    public DialogueHandler.CharacterDialogue character;
    public List<Duel> duels = new();

    private void Update()
    {
        Duel duel = character switch
        {
            DialogueHandler.CharacterDialogue.Guide => duels[0],
            DialogueHandler.CharacterDialogue.Informant => duels[1],
            _ => duels[0]
        };

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
