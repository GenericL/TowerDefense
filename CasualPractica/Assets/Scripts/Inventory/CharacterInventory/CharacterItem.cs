using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CharacterItem", menuName = "Inventory/CharacterItem", order = 1)]
public class CharacterItem : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _selectionSprite;
    [SerializeField] private Sprite _portraitSprite;
    [SerializeField] private GameObject _characterGameObject;

    public string Name {  get { return _name; } }
    public Sprite SelectionSprite { get { return _selectionSprite; } }
    public Sprite PortraitSprite { get {return _portraitSprite; } }
    public GameObject CharacterGameObject { get { return _characterGameObject; } }

}
