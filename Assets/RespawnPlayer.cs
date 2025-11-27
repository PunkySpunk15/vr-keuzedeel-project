using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject RespawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();

            if (cc != null)
                cc.enabled = false;

            other.transform.position = RespawnPoint.transform.position;

            if (cc != null)
                cc.enabled = true;
        }
    }
}
