using UnityEngine;

public class hitChecker : MonoBehaviour
{
    public GameObject target;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "target")
        {
            Debug.Log("Collision detected.");
            target.SetActive(false);
        }
    }
}
