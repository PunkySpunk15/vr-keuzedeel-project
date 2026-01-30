using UnityEngine;

public class Drink : MonoBehaviour
{
    public GameObject whiskeyObject;
    public ParticleSystem particleSystem;
    public AudioSource audio;

    void Update()
    {
        if (IsUpsideDown(whiskeyObject.transform))
        {
            particleSystem.Play();
            audio.Play();
        }
        else
            audio.Stop();
    }

    bool IsUpsideDown(Transform objectTransfrom)
    {
        float angle = Vector3.Angle(objectTransfrom.up, Vector3.down);

        return angle > 90;
    }
}
