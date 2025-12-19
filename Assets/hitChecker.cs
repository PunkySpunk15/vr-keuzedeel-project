using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("target"))
        {
            Debug.Log("Collision detected.");
            GameObject target = collision.transform.gameObject;
            target.SetActive(false);
        }
    }
}
