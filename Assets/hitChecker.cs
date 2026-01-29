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
            _timesHit++;

            if (_timesHit == 1)
            {
                dialogueHandler.StartAfterDuelDialogue();
                _timesHit = 0;
            }
        }
    }
}
