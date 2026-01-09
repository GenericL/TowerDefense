using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    private string nameCharacter;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public void SetHUD(Character character)
    {
        nameCharacter = character.GetCharacterData().GetCharacterName();
        
        hpSlider.maxValue = character.GetCharacterData().GetMaxHealth();
        hpSlider.value = character.GetCharacterData().GetCurrentHealth();

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHP(float hp) { 
        hpSlider.value = hp;
        fill.color = gradient.Evaluate(hpSlider.normalizedValue);
    }
}
