using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve curve;
    Vector3 startPos;


    public IEnumerator Shake(float shakeTime)
    {
        Vector3 startPos = transform.localPosition;
        float timeElapsed = 0f;

        while (timeElapsed < shakeTime)
        {
            timeElapsed += Time.deltaTime;
            float strength = curve.Evaluate(timeElapsed / shakeTime);
            transform.localPosition = startPos + Random.insideUnitSphere * strength;

            yield return null;
        }

        transform.localPosition = startPos;
    }
}
