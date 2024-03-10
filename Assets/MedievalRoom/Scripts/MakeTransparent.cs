using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    public Renderer objectRenderer;
    private float fadeDuration; // Duración de la animación de desaparición
    private float waitTimeToStartFadeOut;
    public bool isFading = false;

    private List<Material> materials = new List<Material>();

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    // Llamada para hacer que el objeto desaparezca progresivamente
    public void FadeOut(float fadeDuration, float waitTimeToStartFadeOut)
    {
        if (objectRenderer != null && !isFading)
        {
            isFading = true;
            GetMaterials();
            StartCoroutine(FadeOutCoroutine(fadeDuration, waitTimeToStartFadeOut));
        }
    }

    // Corrutina para realizar la animación de desaparición
    private IEnumerator FadeOutCoroutine(float fadeDuration, float waitTimeToStartFadeOut)
    {
        yield return new WaitForSeconds(waitTimeToStartFadeOut); // Espera x segundos antes de iniciar la animación de desvanecimiento
        
        float startTime = Time.time;
        float elapsedTime = 0f;


        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            foreach (Material mat in materials)
            {
                mat.SetFloat("_Opacity", alpha);
            }

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        isFading = false;
    }

    // Call to make the object fade in progressively
    public void FadeIn(float fadeDuration, float waitTimeToStartFadeIn)
    {
        if (objectRenderer != null && !isFading)
        {
            isFading = true;
            GetMaterials();
            StartCoroutine(FadeInCoroutine(fadeDuration, waitTimeToStartFadeIn));
        }
    }

    // Coroutine to perform the fade-in animation
    private IEnumerator FadeInCoroutine(float fadeDuration, float waitTimeToStartFadeIn)
    {
        yield return new WaitForSeconds(waitTimeToStartFadeIn);

        float startTime = Time.time;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            foreach (Material mat in materials)
            {
                mat.SetFloat("_Opacity", alpha);
            }

            elapsedTime = Time.time - startTime;
            yield return null;
        }

        isFading = false;
    }

    // Get materials from object
    private void GetMaterials()
    {
        materials.Clear();
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            materials.AddRange(renderer.materials);
        }
        else
        {
            Debug.LogWarning("Renderer component not found on the object.");
        }
    }
}