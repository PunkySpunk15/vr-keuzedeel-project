using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private int _timesHit = 0;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("target"))
        {
            GameObject target = collision.transform.gameObject;
            DialogueHandler dialogueHandler = target.GetComponent<Connect>().dh;
            Duel duel = target.GetComponent<Connect>().duel;
            List<FireGun> fg = target.GetComponent<Connect>().fg;

            if (!duel.active)
                return;

            _timesHit++;

            if (_timesHit == 1)
            {
                int index = dialogueHandler.character switch
                {
                    DialogueHandler.Character.Informant => 3,
                    DialogueHandler.Character.Guide => 2,
                    DialogueHandler.Character.Outlaw => 4
                };

                foreach (FireGun gun in fg)
                    gun.allowFire = false;

                dialogueHandler.SetCharacterObject(index);
                dialogueHandler.StartAfterDuelDialogue();
                _timesHit = 0;
            }
        }
    }
}
