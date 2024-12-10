using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    private ObjectPool<Bullet> bulletPool;

    private void Awake() 
    {
        bulletPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);
    }

    private Bullet CreateBullet() 
    {
        Bullet bulletCopy = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletCopy.MyPool = bulletPool; 
        return bulletCopy;
    }

    private void GetBullet(Bullet bullet) 
    {
        bullet.transform.position = transform.position; // Posicionar el objeto en la posición del jugador
        bullet.gameObject.SetActive(true); // Activar el objeto, mostrarlo
    }

    private void ReleaseBullet(Bullet bullet) 
    {
        bullet.gameObject.SetActive(false); // Desactivar el objeto, esconderlo
    }

    private void DestroyBullet(Bullet bullet) 
    {
        Destroy(bullet.gameObject);   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            // Pedir un objeto al pool
            bulletPool.Get();
        }
    }
}
