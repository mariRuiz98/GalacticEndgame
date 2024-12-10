using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private ObjectPool<Bullet> myPool;
    private float timer;

    public ObjectPool<Bullet> MyPool
    {
        get => myPool;
        set => myPool = value;
    }
    
    void Start()
    {
        //Destroy(gameObject, 4);

    }

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        
        timer += Time.deltaTime;
        
        if (timer >= 4) {
            timer = 0;
            myPool.Release(this);
        }
    }
}
