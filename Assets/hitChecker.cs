using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public readonly Duel duel;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("target"))
        {
            Debug.Log("Collision detected.");
            GameObject target = collision.transform.gameObject;
            target.SetActive(false);

            duel.active = false;
        }
    }
}
