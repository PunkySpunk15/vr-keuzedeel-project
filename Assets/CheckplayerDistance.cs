using UnityEngine;

public class CheckplayerDistance : MonoBehaviour
{
    public GameObject player;
    public Vector3 start;
    public float minDistanceMoved;
    public EnableDisable ed;

    public void Start()
    {
        if (player != null)
        {
            start = player.transform.position;
        }
    }

    public void Update()
    {
        if (player != null)
        {
            float distance = (start - player.transform.position).magnitude;
            if (distance > minDistanceMoved)
            {
                ed.Disable();
            }
        }
    }
}
