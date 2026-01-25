using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public float timerCount = 6;
    public TextMeshProUGUI timerTextElement;
    public TextMeshProUGUI textElement;
    public List<FireGun> fg = new();
    public DialogueHandler.Character character;
    public List<Duel> duels = new();

    private bool _retryDuel = false;

    private void Update()
    {
        Duel duel = character switch
        {
            DialogueHandler.Character.Guide => duels[0],
            DialogueHandler.Character.Informant => duels[1],
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
                timerTextElement.text = ((uint)timerCount).ToString();
            }

            if (timerCount <= -2
                && character is not DialogueHandler.Character.Outlaw)
            {
                //Play gun shoot sound
                _retryDuel = true;

                //FAIL!
                duel.active = false;
                textElement.text = "Let's try again.";

                foreach (FireGun gun in fg)
                {
                    gun.allowFire = false;
                }
            }
        }

        if (_retryDuel && timerCount <= -4)
        {
            timerCount = 5;
            duel.StartDuel();
            _retryDuel = false;
        }
    }
}
