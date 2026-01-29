using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public float timerCount = 6;
    public TextMeshProUGUI timerTextElement;
    public TextMeshProUGUI textElement;
    public EnableDisable button;
    public List<FireGun> fg = new();
    public DialogueHandler.Character character;
    public Duel duel;

    private bool _retryDuel = false;

    private void Update()
    {
        if (duel.active)
        {
            if (!_retryDuel)
                timerCount -= Time.deltaTime;

            if (timerCount <= 0)
            {
                foreach (FireGun gun in fg)
                {
                    gun.allowFire = true;
                }
            }
            else
            {
                timerTextElement.text = ((uint)timerCount).ToString();
            }

            if (
                (
                    timerCount <= -3
                    && character is DialogueHandler.Character.Guide
                    )
                ||
                    (
                    timerCount <= -2
                    && character is DialogueHandler.Character.Informant
                    )
               )
            {
                //Play gun shoot sound
                _retryDuel = true;

                duel.active = false;
                textElement.text = "Let's try again.";

                foreach (FireGun gun in fg)
                {
                    gun.allowFire = false;
                }
            }

            if (timerCount <= -1
                && character is DialogueHandler.Character.Outlaw)
            {
                //Play gun shoot sound
                duel.active = false;
                textElement.text = "You lost.";
                textElement.GetComponent<TextMeshProUGUI>().color = timerTextElement.GetComponent<TextMeshProUGUI>().color;

                timerTextElement.text = string.Empty;
                button.Enable();

                foreach (FireGun gun in fg)
                {
                    gun.allowFire = false;
                }
            }
        }

        if (_retryDuel)
        {
            timerCount = 6;
            duel.StartDuel();
            _retryDuel = false;
        }
    }
}
