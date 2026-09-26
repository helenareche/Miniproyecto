using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { //para crear una referencia estática a BulletPool
        get; 
        private set; }
    public GameObject bulletPrefab;
    public int poolSize = 20; //cuantas balas se crean al empezar
    private Queue<GameObject> pool = new Queue<GameObject>(); //cola
   
   //Se ejecuta antes que start(parecido a un constructor)
   void Awake()
    {
        Instance = this; //guarda una referencia a sí mismo

        //Se crean las balas 
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false); //un GmeObject desactivado no ejecuta Update()
            pool.Enqueue(bullet); //se meten las balas en la cola
        }
    }
   

    public GameObject GetBullet()
    {
        GameObject bullet;
        int disponibles = pool.Count;
        if (disponibles > 0){
            bullet = pool.Dequeue(); //saca la primera bola y la elimina
        }
        else{
            bullet = Instantiate(bulletPrefab, transform); //se crea una bala extra de seguridad
        }
        bullet.SetActive(true); //Se activa la bala para que pueda hacer Update()
        return bullet;
    }

    //método para cuando la bala olisiona o se le acaba el tiempo de vida
    public void ReturnBullet(GameObject bullet){
        bullet.SetActive(false);
        bullet.transform.SetParent(transform);
        pool.Enqueue(bullet); //la vuelve a poner en la cola
    }
}
