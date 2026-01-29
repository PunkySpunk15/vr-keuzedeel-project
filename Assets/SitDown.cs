using UnityEngine;

public class SitDown : MonoBehaviour
{
    public GameObject player;
    public GameObject sitPoint;
    public GameObject grabToMove;
    public GameObject chair;

    public void SitPlayerDown()
    {
        CheckplayerDistance checkplayerDistance = chair.GetComponent<CheckplayerDistance>();
        if (checkplayerDistance == null)
            return;

        checkplayerDistance.enabled = false;

        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.transform.position = new Vector3(sitPoint.transform.position.x, player.transform.position.y, sitPoint.transform.position.z);

        if (cc != null)
            cc.enabled = true;

        chair.GetComponent<CheckplayerDistance>().enabled = false;
        grabToMove.SetActive(false);
    }

    public void GetUp()
    {
        CheckplayerDistance checkplayerDistance = chair.GetComponent<CheckplayerDistance>();
        if (checkplayerDistance == null)
            return;

        checkplayerDistance.enabled = true;
        grabToMove.SetActive(true);
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
