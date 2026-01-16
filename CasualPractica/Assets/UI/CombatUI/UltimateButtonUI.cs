using System;
using UnityEngine;
using UnityEngine.UI;

public class UltimateButtonUI : MonoBehaviour
{
    [SerializeField] private Image ultimateIcon;

    [SerializeField] private Slider energySlider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    public event Action OnUltimateButtonPressed;

    public void OnClicked()
    {
        OnUltimateButtonPressed?.Invoke();
    }
}
