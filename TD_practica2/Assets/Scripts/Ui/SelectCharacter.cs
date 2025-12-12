using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectCharacter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIDocument ui;
    [SerializeField] private List<PlayableCharacter> characters;
    [SerializeField] private CurrencyManager _currencyManager;
    [SerializeField] private GridPlacementCombatantSystem _gridPlacementCombatantSystem;

    [Header("Local")]
    private VisualElement root;
    private VisualElement combatPanel;
    private Label currency;
    private VisualElement charactersListButtons;
    private Button characterButton;
    private Button secondCharacterButton;

    private void Awake()
    {
        root = ui.rootVisualElement;

        charactersListButtons = root.Q<VisualElement>("PrincipalPanel");
        characterButton = charactersListButtons.Q<Button>("InstiantateUnit");
        characterButton.text = characters[0].name;
        characterButton.RegisterCallback<ClickEvent>(ev => selectCharacter(0));
        secondCharacterButton = charactersListButtons.Q<Button>("InstiantateUnitTwo");
        secondCharacterButton.text = characters[1].name;
        secondCharacterButton.RegisterCallback<ClickEvent>(ev => selectCharacter(1));

        combatPanel = root.Q<VisualElement>("CombatPanel");
        currency = combatPanel.Q<Label>("Currency");

    }

    private void FixedUpdate()
    {
        currency.text = _currencyManager.getCurrency().ToString();
    }

    private void selectCharacter(int position)
    {

        _gridPlacementCombatantSystem.setCharacter(characters[position]);
        
    }

}
