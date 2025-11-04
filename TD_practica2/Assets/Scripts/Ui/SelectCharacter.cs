using UnityEngine;
using UnityEngine.UIElements;

public class SelectCharacter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIDocument ui;
    [SerializeField] private GameObject character;

    [Header("Local")]
    private VisualElement root;
    private VisualElement charactersListButtons;
    private Button characterButton;

    private void Awake()
    {
        root = ui.rootVisualElement;

        charactersListButtons = root.Q<VisualElement>("PrincipalPanel");
        characterButton = charactersListButtons.Q<Button>("InstiantateUnit");
        characterButton.RegisterCallback<ClickEvent>(ev => createCharacter());

    }

    private void createCharacter()
    {
        Instantiate(character, new Vector3(1,1,0),Quaternion.identity);
    }

}
