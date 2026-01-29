using UnityEngine;

public class Duel : MonoBehaviour
{
    public bool active = false;

    public void StartDuel()
    {
        active = true;
    }

    public void StopDuel()
    {
        active = false;
    }
}
