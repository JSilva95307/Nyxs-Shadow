using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    private SpriteRenderer sprite;
    
    public Material material;
    public float interval;
    public float duration;

    public float timer = 0;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public IEnumerator StrobeColor()
    {
        Material oldMat = sprite.material;
        timer = 0;

        while(timer < duration)
        {
            sprite.material = material;
            yield return new WaitForSeconds(interval);
            sprite.material = oldMat;
            yield return new WaitForSeconds(interval);
        }

        sprite.material = oldMat;
        yield return null;
    }

}
