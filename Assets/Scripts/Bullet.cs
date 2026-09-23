using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; //velocidad de la bala
    public float maxLifeTime = 3f; //tiempo de vida de la bala
    public Vector3 targetVector; //direccion bala

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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            //IncreaseScore(); //cada vez que se destruya un meteorito
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
}
