using UnityEngine;

public class SitDown : MonoBehaviour
{
    public GameObject player;
    public GameObject sitPoint;
    public GameObject grabToMove;

    public void SitPlayerDown()
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.transform.position = sitPoint.transform.position;

        if (cc != null)
            cc.enabled = true;

        grabToMove.SetActive(false);
    }

    public void RotatePlayer()
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.transform.rotation = sitPoint.transform.rotation;

        if (cc != null)
            cc.enabled = true;
    }
}
