using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartUIButtonNavigation : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private Button startButton;
    private Button exitButton;
    void Awake()
    {
        VisualElement root = uiDocument.rootVisualElement;
        VisualElement content = root.Q<VisualElement>();
        VisualElement buttons = root.Q<VisualElement>();
        startButton = buttons.Q<Button>("StartButton");
        startButton.RegisterCallback<ClickEvent>(ev => GoToSelectionScene());
        exitButton = buttons.Q<Button>("ExitButton");
        exitButton.RegisterCallback<ClickEvent>(ev => ExitGame());
    }

    private void GoToSelectionScene()
    {
        SceneManager.LoadScene(UiConstants.CHOSE_THE_COMBATANT_SCENE);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
