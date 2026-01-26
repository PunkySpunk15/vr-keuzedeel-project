using UnityEngine;

public class CheckplayerDistance : MonoBehaviour
{
    public GameObject player;
    public GameObject start;
    public float minDistanceMoved;
    public EnableDisable ed;
    public bool isCanvas = false;

    public void Update()
    {
        if (player != null)
        {
            float distance = (start.transform.position - player.transform.position).magnitude;

            if (distance < minDistanceMoved)
            {
                if (isCanvas)
                    ed.Enable();
                else
                    ed.Disable();
            }

            if (distance > minDistanceMoved && isCanvas)
                ed.Disable();
        }
    }
}
