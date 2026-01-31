using UnityEngine;

public class SitDown : MonoBehaviour
{
    public GameObject player;
    public GameObject sitPoint;
    public EnableDisable grabToMove;
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
        grabToMove.Disable();

        if (cc != null)
            cc.enabled = true;

        chair.GetComponent<CheckplayerDistance>().enabled = false;
    }

    public void GetUp()
    {
        CheckplayerDistance checkplayerDistance = chair.GetComponent<CheckplayerDistance>();
        if (checkplayerDistance == null)
            return;

        checkplayerDistance.enabled = true;
        grabToMove.Enable();
    }

    public void RotatePlayer()
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.transform.rotation = new Quaternion(sitPoint.transform.rotation.x, 0f, sitPoint.transform.rotation.z, sitPoint.transform.rotation.w);

        if (cc != null)
            cc.enabled = true;
    }
}
