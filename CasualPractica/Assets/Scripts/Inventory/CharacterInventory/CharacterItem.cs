using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "CharacterItem", menuName = "Inventory/CharacterItem", order = 1)]
public class CharacterItem : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Image _selectionSprite;
    [SerializeField] private Image _portraitSprite;
    [SerializeField] private GameObject _characterGameObject;

    public string Name {  get { return _name; } }
    public Image SelectionSprite { get { return _selectionSprite; } }
    public Image PortraitSprite { get {return _portraitSprite; } }
    public GameObject CharacterGameObject { get { return _characterGameObject; } }

}
