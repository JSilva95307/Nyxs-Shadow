using System.Collections;
using UnityEngine;

public class AfterimageGenerator : MonoBehaviour
{
    public bool generate = false;

    public GameObject afterimage;
    public SpriteRenderer spriteRendererToCopyFrom;
    public float spawnFrequency;
    public float fadeSpeed;

    private Coroutine spawnCoroutine;


    
    public void Play()
    {
        if(spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(Spawn());
    }

    public void Stop()
    {
        StopCoroutine(spawnCoroutine);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);
            InstansiateAfterimage();
        }
        
    }


    private void InstansiateAfterimage()
    {
        //GameObject
        GameObject go = Instantiate(afterimage);
        //go.transform.SetParent(this.transform);
        go.transform.position = spriteRendererToCopyFrom.transform.position;
        go.transform.rotation = spriteRendererToCopyFrom.transform.rotation;
        go.layer = spriteRendererToCopyFrom.gameObject.layer;
        go.transform.localScale = spriteRendererToCopyFrom.transform.localScale;
        afterimage.GetComponent<Afterimage>().fadeSpeed = fadeSpeed;

        //Sprite
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = spriteRendererToCopyFrom.sprite;
        sr.material = spriteRendererToCopyFrom.material;
        sr.sortingLayerID = spriteRendererToCopyFrom.sortingLayerID;
        sr.sortingLayerName = spriteRendererToCopyFrom.sortingLayerName;
        sr.sortingOrder = spriteRendererToCopyFrom.sortingOrder-1;
        sr.flipX = spriteRendererToCopyFrom.flipX;
        sr.flipY = spriteRendererToCopyFrom.flipY;

    }
}
