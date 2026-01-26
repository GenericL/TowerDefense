using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttacksButtons: MonoBehaviour
{
    [SerializeField] private Button basicAttackButton;
    [SerializeField] private Button abilityAttackButton;
    [SerializeField] private TextMeshProUGUI abilityPointUI;

    private event UnityAction onBasicAttack;
    private event UnityAction onAbilityAttack;

    public void Initialize(UnityAction onBasicAttack, UnityAction onAbilityAttack, AbilityPointSystem abilityPointSystem)
    {
        this.onBasicAttack = onBasicAttack;
        this.onAbilityAttack = onAbilityAttack;

        abilityPointSystem.AddAbilityPointEvent(setAP);
    }

    public void setAP(int point)
    {
        abilityPointUI.text = "AP: " + point;
    }

    public void DisableButtons()
    {
        basicAttackButton.gameObject.SetActive(false);
        abilityAttackButton.gameObject.SetActive(false);
    }
    public void EnableButtons()
    {
        basicAttackButton.gameObject.SetActive(true);
        abilityAttackButton.gameObject.SetActive(true);
    }

    public void OnBasicAttackButton()
    {
        onBasicAttack?.Invoke();
    }
    public void OnAbilityAttackButton()
    {
        onAbilityAttack?.Invoke();
    }

    public void SetColors(Color color)
    {
        basicAttackButton.image.color = color;
        abilityAttackButton.image.color = color;
    }

    public void OnDisable()
    {
        basicAttackButton.onClick.RemoveAllListeners();
        abilityAttackButton.onClick.RemoveAllListeners();
    }
}
