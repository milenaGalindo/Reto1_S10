using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionsAdmin : MonoBehaviour
{

    internal bool esInvencible;
    public static int puntuacion;
    public GameObject TextObject;
    public TextMeshProUGUI TextComponent;
    private int deadGhosts;
    public GameObject restarButton;
    public GameObject cherry;
    private bool tieneCereza;
    public Material winSkybox;




    private void Start()
    {
        deadGhosts = 0;
        esInvencible = false;
        CollisionsAdmin.puntuacion = 0;
        deadGhosts = 0;
        gameObject.GetComponent<Rigidbody>().sleepThreshold = 0.0f;
        tieneCereza =false;

    }


    private void Update()
    {
        if(CollisionsAdmin.puntuacion == 15 && tieneCereza == false) 
        {
            cherry.SetActive(true);
            tieneCereza = true;

        }

        if (deadGhosts == 5)
        {
            TextComponent.rectTransform.sizeDelta = new Vector2(1000f, 0f);
            TextComponent.rectTransform.localPosition = new Vector2(0f, 0f);
            TextComponent.fontSize = 150f;
            //TextComponent.characterSpacing = 60f;
            //TextComponent.lineSpacing = 20f;
            TextComponent.color = new Vector4(0.5964171f, 1f, 0.345098f, 1f); 
            TextObject.GetComponent<TMP_Text>().fontMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, 0.2f);
            TextObject.GetComponent<TMP_Text>().fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.1f);
            TextComponent.SetText("Win");
            RenderSettings.skybox = winSkybox;
            DynamicGI.UpdateEnvironment();

        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {

            if (!esInvencible)
            {
                StartCoroutine("PlayerDeath");
                TextMeshProUGUI TextComponent = TextObject.GetComponent<TextMeshProUGUI>();
                TextObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0); ;



                TextComponent.rectTransform.sizeDelta = new Vector2(1000f,100f);
                TextComponent.fontSize = 150f;
                TextComponent.characterSpacing = 60f;
                TextComponent.lineSpacing = 20f;
                TextComponent.color = new Vector4(0.8490566f, 0.04932706f,0f,1f);
                TextObject.GetComponent<TMP_Text>().fontMaterial.SetFloat(ShaderUtilities.ID_OutlineSoftness, 0.2f);
                TextObject.GetComponent<TMP_Text>().fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.1f);
                TextComponent.SetText("Game Over");
                restarButton.SetActive(true);

            }

            if (esInvencible)
            {
                if (deadGhosts != 5) deadGhosts++;



            }

        }       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DotSpheres"))
        {
            puntuacion++;

            TextObject.GetComponent<TextMeshProUGUI>().SetText(puntuacion.ToString());
            //TextComponent.text = CollisionsAdmin.puntuacion.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Cherry"))
        {
            esInvencible = true;
            Destroy(other.gameObject);
            StartCoroutine("cherryEffect");
        }

        if (other.gameObject.CompareTag("LeftHall"))
        {
            gameObject.transform.position = new Vector3(10f ,0.5f,-0.5f);
        }

        if (other.gameObject.CompareTag("RightHall"))
        {
            gameObject.transform.position = new Vector3(-10f, 0.5f, -0.5f);
        }

    }


    IEnumerator PlayerDeath()
    {
        gameObject.GetComponent<Move>().enabled = false;

        float elapsedTime = 0;
        float waitTime = 0.5f;
        Vector3 originalScale = transform.localScale;
        Vector3 finalScale = new Vector3(0.7f, 0f, 0.7f);

        Vector4 originalEmission = gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        Vector4 finalEmission = originalEmission * -3f;

        while (elapsedTime < waitTime)
        {
            transform.localScale = Vector3.Lerp(originalScale, finalScale, (elapsedTime / waitTime));
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(originalEmission, finalEmission, (elapsedTime / waitTime)));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator cherryEffect()
    {
        float elapsedTime = 0;
        float waitTime = 6f;
        Vector4 originalEmission = gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        Vector4 finalEmission = originalEmission * 10f;
        //bool test = false; 

        while (elapsedTime < waitTime)
        {

            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(originalEmission, finalEmission, (elapsedTime / waitTime)));

            elapsedTime += Time.deltaTime;
            yield return null;

        }
        /*
        yield return new WaitWhile(() => test == true);
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", originalEmission);
        esInvencible = false;
        */
    }




}
