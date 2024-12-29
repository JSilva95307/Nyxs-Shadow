using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Cooldowns : MonoBehaviour
{
    public float cooldownTime;
    private double cooldownProgress;
    private float startTime;
    private float endTime;
    private float cur;
    private bool waiting = true;

    public UnityEvent timerFinished;
    public UnityEvent progressUpdate;

    private void Awake()
    {
        timerFinished = new UnityEvent();
        cur = 0;
    }

    public void SetCooldown(float cooldown)
    { 
        cooldownTime = cooldown;
    }

    public void ResetCooldown()
    {
        endTime = Time.time + cooldownTime;
    }

    public void StartCooldown(UnityAction func)
    {
        startTime = Time.time;
        endTime = startTime + cooldownTime;
        waiting = false;
        timerFinished.AddListener(func);
    }

    private void Update()
    {
        if (cur / endTime <= 1 && !waiting)
        {
            cur += Time.deltaTime;
            cooldownProgress = cur / endTime;
        }
        else
        {
            cooldownProgress = 1;
            if (!waiting)
            { 
                timerFinished.Invoke();
                cur = 0;
            }
            waiting = true;
        }
    }

    public double GetProgress()
    {
        return cooldownProgress;
    }
}
