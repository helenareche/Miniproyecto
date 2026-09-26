using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [Header("Referencias UI")]
    public GameObject panelPausa; //panel de pausa con boton de reanudar

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f; //paramos escena
        GameIsPaused = true;
    }

    public void Reanudar()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f; //reanudamos escena
        GameIsPaused = false;
    }
}
