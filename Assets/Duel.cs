using UnityEngine;

public class Duel : MonoBehaviour
{
    public bool active = false;
    public AudioSource duelMusic;
    public AudioSource genericMusic;

    public void StartDuel()
    {
        active = true;
        if (!duelMusic.isPlaying)
            duelMusic.Play();

        genericMusic.Stop();
    }

    public void StopDuel()
    {
        active = false;
        duelMusic.Stop();
        genericMusic.Play();
    }
}
