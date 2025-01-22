using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Cooldowns : MonoBehaviour
{
    //progress report var
    private double cooldownProgress;
    // functionality vars
    private float cdTimerCounter;
    public float cooldownTime;
    private float startTime;
    private float endTime;
    private bool waitingForInput = true;

    public UnityEvent timerFinished;

    private void Awake()
    {
        timerFinished = new UnityEvent();
    }

    public void SetCooldown(float cooldown)
    { 
        cooldownTime = cooldown;
        cdTimerCounter = cooldown;
    }

    public void ResetCooldown()
    {
        endTime = Time.time + cooldownTime;
    }

    public void StartCooldown(UnityAction func)
    {
        startTime = Time.time;
        endTime = startTime + cooldownTime;
        waitingForInput = false;
        timerFinished.AddListener(func);
    }

    private void Update()
    {
        if (cdTimerCounter > 0 && !waitingForInput)
        {
            cdTimerCounter -= Time.deltaTime;
            cooldownProgress = (1 - cdTimerCounter) / cooldownTime;
        }
        else
        {
            cooldownProgress = 1;
            if (!waitingForInput)
            { 
                timerFinished.Invoke();
                cdTimerCounter = cooldownTime;
            }
            waitingForInput = true;
        }
    }

    public double GetProgress()
    {
        return cooldownProgress;
    }
}
