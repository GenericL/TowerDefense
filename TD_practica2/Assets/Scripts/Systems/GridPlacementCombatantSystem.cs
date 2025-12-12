using CodeMonkey.Utils;
using UnityEngine;

public class GridPlacementCombatantSystem : MonoBehaviour
{
    private PlayableCharacter _playableCharacter;
    [SerializeField] private CurrencyManager _currencyManager;
    private Grid<GridObject> grid;

    private void Awake()
    {
        int gridWidth = 10;
        int gridHeight = 5;
        float cellSize = 1f;
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-4.5f,0,-2.5f), (Grid<GridObject> g, int x, int z) => new GridObject(g,x,z));
    }

    public class GridObject
    {
        private Grid<GridObject> grid;
        public int x;
        public int z;
        private Transform transform;

        public GridObject(Grid<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x,z);
        }

        public void ClearTransform()
        {
            this.transform = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanPlace()
        {
            return transform == null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + transform;
        }
    }

    private void Update()
    {
        if (_playableCharacter != null && Input.GetMouseButtonDown(0))
        {
            grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);
            GridObject gridObject = grid.GetGridObject(x, z);
            if (gridObject.CanPlace() && _currencyManager.useCurrency(_playableCharacter.cost))
            {
                Transform placeTransform = Instantiate(_playableCharacter.characterModel, grid.GetWorldPosition(x, z), Quaternion.identity);
                gridObject.SetTransform(placeTransform);
            }
            else
            {
                UtilsClass.CreateWorldTextPopup("Can't place combatant there", Mouse3D.GetMouseWorldPosition());
            }
        }
    }

    public void setCharacter(PlayableCharacter character)
    {
        _playableCharacter = character;
    }

}
