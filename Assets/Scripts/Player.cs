//using Unity.ProjectAuditor.Editor.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Parametros para la velocidad del objeto
    //Un atributo con public permite editar su valor dentor de la escena en Unity
    public float thrustForce = 10f; //fuerza de empuje
    public float rotationSpeed = 120f; //velocidad de rotación
    
    public GameObject gun, bulletPrefab; //objetos de la Nave, pistola y balas
    private Rigidbody _rigid; //Nave

    public static int SCORE = 0;
    public static float xBorderLimit, yBorderLimit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Para la configuracion inicial del objeto
        _rigid = GetComponent<Rigidbody>(); //inicializamos el objeto nave

        yBorderLimit = Camera.main.orthographicSize + 1;
        xBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width/Screen.height ;
    }

    // Update is called once per frame
    void Update()
    {
        //Se ejecuta al final de cada frame del juego
       float thrust = Input.GetAxis("Vertical") * Time.deltaTime;   //deteccion del movimiento cuando el usuario juega
       float rotation = Input.GetAxis("Horizontal") * Time.deltaTime; 
       Vector3 thrustDirection = transform.right; //porque la cabeza de la nave apunta a la derecha

       _rigid.AddForce(thrustDirection * thrust * thrustForce);
       transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        //Universo infinito
        var newPos = transform.position;
        if(newPos.x > xBorderLimit)
        {
            newPos.x = -xBorderLimit + 1;
        }
        else if(newPos.x < -xBorderLimit)
        {
            newPos.x = xBorderLimit - 1;
        }
        else if(newPos.y > yBorderLimit)
        {
            newPos.y = -yBorderLimit + 1;
        }
        else if(newPos.y < -yBorderLimit)
        {
            newPos.y = yBorderLimit - 1;
        }
        transform.position = newPos;

       if(Input.GetKeyDown(KeyCode.Space))//preguntamos si estamos pulsado el boton de disparar
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity); //instanciar bala
            Bullet balaScript = bullet.GetComponent<Bullet>(); //para que las balas tenga la direccion de la Nave
            balaScript.targetVector = transform.right;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisonado con otra cosa...");
        }
    }
}
