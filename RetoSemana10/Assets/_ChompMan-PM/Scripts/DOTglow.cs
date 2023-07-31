using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOTglow : MonoBehaviour
{
    public GameObject obtenerEmision; 
    private Material materialEmission;
    private Vector4 origEmmision;
    private Light luz;


    public float potenciaLuzMaxima;
    public float potenciaLuzMinima;
    public float emissionIntensity;

    public float velocidadBrillo;
    public float contador;

    

    
    private bool cicloCompleto;


    private void Start()
    {        
        luz = gameObject.GetComponent<Light>();
        materialEmission = obtenerEmision.GetComponent<Renderer>().material;
        origEmmision = materialEmission.GetColor("_EmissionColor");

        contador = 0;
        cicloCompleto = true;
    }


    void Update()
    {
        if (cicloCompleto)
        {           

            luz.intensity = Mathf.Lerp(potenciaLuzMinima, potenciaLuzMaxima, contador / velocidadBrillo);
            materialEmission.SetColor("_EmissionColor", Color.Lerp(origEmmision, origEmmision * emissionIntensity, contador / velocidadBrillo));
            contador += Time.deltaTime;

            if (contador >= velocidadBrillo)
            {
                cicloCompleto = false;
                contador = 0;
            }
        }

        else if (!cicloCompleto)
        {
  
            luz.intensity = Mathf.Lerp(potenciaLuzMaxima, potenciaLuzMinima, contador / velocidadBrillo);
            materialEmission.SetColor("_EmissionColor", Color.Lerp(origEmmision * emissionIntensity, origEmmision, contador / velocidadBrillo));
                     
            contador += Time.deltaTime;

            if (contador >= velocidadBrillo)
            {
                contador = 0;
                cicloCompleto = true;
            }
            
        }
    }
}
