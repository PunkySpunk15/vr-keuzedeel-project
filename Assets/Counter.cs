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
    public AudioSource gunShot;

    private DialogueHandler _dh;
    private bool _retryDuel = false;
    private int _timesFailed = 0;

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
                gunShot.Play();
                if (character is DialogueHandler.Character.Informant)
                    _dh.SetCharacterObject(2);

                _retryDuel = true;

                duel.active = false;
                textElement.text = "Let's try again.";

                foreach (FireGun gun in fg)
                {
                    gun.allowFire = false;
                }

                ++_timesFailed;
            }

            if (timerCount <= -1
                && character is DialogueHandler.Character.Outlaw)
            {
                gunShot.Play();
                _dh.SetCharacterObject(3);
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

            if (_timesFailed > 2)
                button.Enable();
        }

        if (_retryDuel)
        {
            timerCount = 6;
            duel.StartDuel();
            _retryDuel = false;
        }
    }
}
