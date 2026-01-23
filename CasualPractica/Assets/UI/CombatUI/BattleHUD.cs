using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Gradient hpGradient;
    [SerializeField] private Image hpFill;
    [SerializeField] private Image characterImage;

    [SerializeField] private Image ultimateIcon;

    [SerializeField] private Slider energySlider;
    [SerializeField] private Gradient energyGradient;
    [SerializeField] private Image energyFill;

    public event UnityAction OnUltimateButtonPressed;

    public void SetHUD(Playable character, Action<Playable> onUltimateAttackButton)
    {
        hpSlider.maxValue = character.GetCharacterData().GetMaxHealth();
        hpSlider.value = character.GetCharacterData().GetCurrentHealth();
        energySlider.maxValue = character.EnergySystem.GetMaxEnergy();
        energySlider.value = character.EnergySystem.GetCurrentEnergy();

        hpFill.color = hpGradient.Evaluate(1f);
        energyFill.color = energyGradient.Evaluate(1f);

        character.GetCharacterData().SetHealthEvent(SetHP);
        character.EnergySystem.SetEnergyEvent(SetEnergy);

        AddUltimateEvent(character, onUltimateAttackButton);
    }

    public void SetHP(float hp) { 
        hpSlider.value = hp;
        hpFill.color = hpGradient.Evaluate(hpSlider.normalizedValue);
    }
    public void SetEnergy(int energy) 
    { 
        energySlider.value = energy;
        energyFill.color = energyGradient.Evaluate(energySlider.normalizedValue);
    }
    private void AddUltimateEvent(Playable character, Action<Playable> ultimate) => OnUltimateButtonPressed += () => ultimate(character);

    public void OnClicked()
    {
        OnUltimateButtonPressed?.Invoke();
    }

    internal void SetHUD(Playable character, object v)
    {
        throw new NotImplementedException();
    }
}
