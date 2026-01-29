using UnityEngine;

public class Drink : MonoBehaviour
{
    public GameObject whiskeyObject;
    public ParticleSystem particleSystem;

    void Update()
    {
        if (IsUpsideDown(whiskeyObject.transform))
            particleSystem.Play();
    }

    bool IsUpsideDown(Transform objectTransfrom)
    {
        float angle = Vector3.Angle(objectTransfrom.up, Vector3.down);

        return angle > 90;
    }
}
