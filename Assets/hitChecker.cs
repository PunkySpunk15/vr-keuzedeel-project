using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private int _timesHit = 0;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("target"))
        {
            Debug.Log("Collision detected.");

            GameObject target = collision.transform.gameObject;
            DialogueHandler dialogueHandler = target.GetComponent<DialogueHandler>();
            _timesHit++;

            if (_timesHit == 1)
            {
                dialogueHandler.StartAfterDuelDialogue();
                _timesHit = 0;
            }
        }
    }
}
