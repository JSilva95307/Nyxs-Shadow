using UnityEngine;
using UnityEngine.WSA;

public class LaunchScript : MonoBehaviour
{
    [SerializeField]
    protected Transform launchableObject;
    [SerializeField]
    [Header("Launch Vars")]
    public Vector2 launchDir;
    public float launchForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyLaunch()
    {
        if (launchDir != Vector2.zero && launchForce > 0)
        {
            transform.Translate(launchDir * launchForce * Time.deltaTime);
            launchForce -= (launchForce * 0.05f);
            if (launchForce <= 0.1f)
                launchForce = 0f;
        }
    }
}
