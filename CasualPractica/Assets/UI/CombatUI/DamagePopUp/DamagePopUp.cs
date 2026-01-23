using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    // Create a Damage Popup
    public static DamagePopUp Create(Vector3 position, float damageAmount, bool isCriticalHit, Color elementalType)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopUp, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopupTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount, isCriticalHit, elementalType);

        return damagePopUp;
    }

    // Variables used in the popup
    private const float DISAPPEAR_TIMER_MAX = 1f;

    private static int _sortingOrder;

    private TextMeshPro _textMesh;
    private float _disappearTimer;
    private Color _textColor;
    private Vector3 _moveVector;

    // Obtain the component
    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    // Setting up the variables of the popup
    public void Setup(float damageAmount, bool isCriticalHit, Color elementalType)
    {
        _textMesh.SetText(damageAmount.ToString());

        if (isCriticalHit)
        {
            _textMesh.fontSize = 15;
            _textColor = elementalType;
        }
        else
        {
            _textMesh.fontSize = 10;
            _textColor = elementalType;
        }

        _textColor = _textMesh.color;
        _disappearTimer = DISAPPEAR_TIMER_MAX;

        // Put it above other popups
        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;

        _moveVector = new Vector3(1, 1) * 10f;
    }

    private void Update()
    {
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * 8f * Time.deltaTime;

        if (_disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            // First half of the popup life
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            // Second half of the popup life
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        // Making the popup disappear with time
        _disappearTimer -= Time.deltaTime;

        if (_disappearTimer < 0)
        {
            float dissapearSpeed = 3f;
            _textColor.a -= dissapearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if (_textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

