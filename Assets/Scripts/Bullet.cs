using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; //velocidad de la bala
    public float maxLifeTime = 3f; //tiempo de vida de la bala
    public Vector3 targetVector; //direccion bala

    public GameObject miniAsteroidPrefab; 
    public float splitAngle = 45f;       
    public float miniSpeed = 3f;          


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, maxLifeTime); //funcion para destruir la bala
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("MiniEnemy"))
        {
            IncreaseScore(); //cada vez que se alcanza un meteorito
            //Si el meteorito es grande se ddescompone en dos mini asteroides
            if(collision.gameObject.CompareTag("Enemy")){
                MiniAsteroids(collision.transform.position);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void IncreaseScore()
    {
        //incrementar el score (variable de player porque es un elemento que no "muere")
        Player.SCORE++;
        Debug.Log(Player.SCORE);

        UpdateScoreText();

    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }

    //Método que hace que aparezcan 2 mini asteroides por colisión 
    private void MiniAsteroids(Vector3 spawnPosition){
        // El vector de la bala  es la bisectriz de los dos mini-asteroides
        Vector3 dirRight = Quaternion.Euler(0, 0, splitAngle) * targetVector; //rota el vector de dirección de la bala a un angulo de 45 grados
        Vector3 dirLeft  = Quaternion.Euler(0, 0, -splitAngle) * targetVector; //lo mismo pero con el ángulo negativo

        CreateMiniAsteroid(spawnPosition, dirRight);
        CreateMiniAsteroid(spawnPosition, dirLeft);
    }

    private void CreateMiniAsteroid(Vector3 position, Vector3 direction)
    {
        GameObject mini = Instantiate(miniAsteroidPrefab, position, Quaternion.identity); //Instatntiate clona un prefab y lo mete en la escena como un GameObject
        mini.tag = "MiniEnemy";

        Rigidbody miniRb = mini.GetComponent<Rigidbody>();
        if (miniRb != null) {
            miniRb.linearVelocity = direction.normalized * miniSpeed; //movimiento del mini asteroide
        }
    }
}
