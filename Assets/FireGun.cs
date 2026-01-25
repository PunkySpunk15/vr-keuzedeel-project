using UnityEngine;

public class FireGun : MonoBehaviour
{
    public GameObject PrefabBullet;
    public GameObject BulletSpawnPoint;
    public bool allowFire = false;

    public void fireGun()
    {
        if (!allowFire)
            return;

        GameObject bullet = Instantiate(PrefabBullet, BulletSpawnPoint.transform.position, this.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = (-bullet.transform.forward) * 15f;
    }
}
