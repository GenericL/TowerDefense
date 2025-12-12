using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int currency = 0;
    private readonly float generatorBaseCd = 3f;
    private float generatorCd = 3f;

    private void Update()
    {
        if (generatorCd < 0)
        {
            currency++;
            generatorCd = generatorBaseCd;
        }
        else
            generatorCd -= Time.deltaTime;
    }


    public int getCurrency()
    {
        return currency;
    }

    public bool useCurrency(int moneyNeeded)
    {
        if (currency >= moneyNeeded)
        {
            currency -= moneyNeeded;
            return true;
        }
        return false;
    }
}
