using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SelectCombatantUIScript: MonoBehaviour
{

    [SerializeField] private UIDocument uiDocument;

    private Button firstPositionButton;
    private Button secondPositionButton;
    private Button thirdPositionButton;
    private Button fourthPositionButton;

    private Button startButton;
    private Button exitButton;

    void Awake()
    {
        VisualElement root = uiDocument.rootVisualElement;
        VisualElement content = root.Q<VisualElement>();

        VisualElement selectionPanel = root.Q<VisualElement>("SelectionPanel");

        firstPositionButton = selectionPanel.Q<Button>("FirstPosition");
        firstPositionButton.RegisterCallback<ClickEvent>(ev => GoToInventory());

        secondPositionButton = selectionPanel.Q<Button>("SecondPosition");
        secondPositionButton.RegisterCallback<ClickEvent>(ev => GoToInventory());

        thirdPositionButton = selectionPanel.Q<Button>("ThirdPosition");
        thirdPositionButton.RegisterCallback<ClickEvent>(ev => GoToInventory());

        fourthPositionButton = selectionPanel.Q<Button>("FourthPosition");
        fourthPositionButton.RegisterCallback<ClickEvent>(ev => GoToInventory());

        VisualElement buttons = root.Q<VisualElement>("ButtonPanel");
        startButton = buttons.Q<Button>("StartButton");
        startButton.RegisterCallback<ClickEvent>(ev => GoToCombat());
        exitButton = buttons.Q<Button>("ExitButton");
        exitButton.RegisterCallback<ClickEvent>(ev => GoToMainMenu());
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(UiConstants.START_MENU_SCENE);
    }

    private void GoToCombat()
    {
        SceneManager.LoadScene(UiConstants.COMBAT_SCENE);
    }

    private void GoToInventory()
    {

    }
}
