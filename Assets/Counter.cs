using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private float _timerCount = 11;
    public TextMeshProUGUI timerTextElement;
    public TextMeshProUGUI textElement;
    public EnableDisable button;
    public List<FireGun> fg = new();
    public DialogueHandler.Character character;
    public Duel duel;
    public AudioSource gunShot;
    public DialogueHandler dh;

    private bool _retryDuel = false;

    private void Update()
    {
        if (duel.active)
        {
            if (!_retryDuel)
                _timerCount -= Time.deltaTime;

            if (_timerCount <= 0)
                foreach (FireGun gun in fg)
                    gun.allowFire = true;
            else
                timerTextElement.text = ((uint)_timerCount).ToString();

            if (_timerCount <= 4
                && character is DialogueHandler.Character.Informant)
                dh.SetCharacterObject(1);

            if (
                (
                    _timerCount <= -3
                    && character is DialogueHandler.Character.Guide
                    )
                ||
                    (
                    _timerCount <= -2
                    && character is DialogueHandler.Character.Informant
                    )
               )
            {
                gunShot.Play();
                if (character is DialogueHandler.Character.Informant)
                    dh.SetCharacterObject(2);

                _retryDuel = true;

                duel.active = false;
                textElement.text = "Let's try again.";

                foreach (FireGun gun in fg)
                    gun.allowFire = false;
            }

            if (_timerCount <= -1
                && character is DialogueHandler.Character.Outlaw)
            {
                gunShot.Play();
                dh.SetCharacterObject(3);
                duel.active = false;
                textElement.text = "You lost.";
                textElement.GetComponent<TextMeshProUGUI>().color = timerTextElement.GetComponent<TextMeshProUGUI>().color;

                timerTextElement.text = string.Empty;
                button.Enable();

                foreach (FireGun gun in fg)
                    gun.allowFire = false;
            }
        }

        if (_retryDuel)
        {
            _timerCount = 6;
            duel.StartDuel();
            _retryDuel = false;
        }
    }
}
