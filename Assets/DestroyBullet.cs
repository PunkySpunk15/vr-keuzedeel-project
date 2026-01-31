using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private float _bulletAliveTime = 0;

    void Update()
    {
        if (this.gameObject.CompareTag("bullet")
            && this.gameObject.activeSelf)
            _bulletAliveTime += Time.deltaTime;

        if (_bulletAliveTime >= 2)
            Destroy(this.gameObject);
    }
}
