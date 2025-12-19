using UnityEngine;

public class FireGun : MonoBehaviour
{
    private int _ammo = 100;
    public GameObject PrefabBullet;
    public GameObject BulletSpawnPoint;
    public bool allowFire = false;

    public void fireGun()
    {
        if (_ammo == 0 || !allowFire)
            return;

        _ammo--;
        GameObject tempGO = Instantiate(PrefabBullet, BulletSpawnPoint.transform.position, this.transform.rotation);
        tempGO.GetComponent<Rigidbody>().velocity = (-tempGO.transform.forward) * 15f;
    }
}
