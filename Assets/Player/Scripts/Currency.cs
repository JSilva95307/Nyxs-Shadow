using UnityEngine;

public class Currency : MonoBehaviour
{
    public float money;


    public float GetMoney()
    {
        return money;
    }

    public void SetMoney(float _money)
    {
        money = _money;
    }
}
