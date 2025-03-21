using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float fadeSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut(fadeSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeOut(float fadeSpeed)
    {
        float alpha = spriteRenderer.color.a;
        Color origColor = spriteRenderer.color;

        while (spriteRenderer.color.a > 0)
        {
            alpha -= Time.deltaTime / fadeSpeed;
            spriteRenderer.color = new Color(origColor.r, origColor.g, origColor.b, alpha);
            yield return null;
        }

        spriteRenderer.color = new Color(origColor.r, origColor.g, origColor.b, 0f);
        Destroy(gameObject);
        //Debug.Log("Destroy");
    }
}
