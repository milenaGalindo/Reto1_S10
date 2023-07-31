using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIbuttonControl : MonoBehaviour
{
    // Start is called before the first frame update

    enum Boton { Empezar, Reintentar};
    [SerializeField]
    Boton tipoBoton = new Boton();
    bool inicio;



    private void start()
    {
        inicio = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (inicio)
        {
            Time.timeScale = 0;
            inicio = false;
        }

        if (tipoBoton == Boton.Empezar) {


            if (Input.GetKeyUp(KeyCode.M))
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }
        }

        if (tipoBoton == Boton.Reintentar)
        {

            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }


    }
}
