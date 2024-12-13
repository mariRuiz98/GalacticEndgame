using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform[] spawnPoints;
    private ObjectPool<Bullet> bulletPool;
    private bool doubleBullet = false;
    

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

    public void EnableDoubleBullet(float time) {
        StartCoroutine(DoubleBullet(time));
    }

    private IEnumerator DoubleBullet(float time) {
        doubleBullet = true;
        yield return new WaitForSeconds(time);
        doubleBullet = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            if(doubleBullet) {
                foreach(Transform spawnPoint in spawnPoints) {
                    bulletPool.Get();
                    Bullet bullet = bulletPool.Get();
                    bullet.transform.position = spawnPoint.position; 
                    bullet.gameObject.SetActive(true);
                }
            }
            else {  
                // Pedir un objeto al pool
                bulletPool.Get();
            }
        }
    }
}
