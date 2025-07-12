using UnityEngine;
using UnityEngine.UI;

public class ChargeKeeper : MonoBehaviour
{
    private static float _charge = 0.0f;
    private static readonly float _maxCharge = 100.0f;
    [SerializeField] private Slider _chargeSlider;
    public static ChargeKeeper Instance;

    private void Awake()
    {
        Instance = this;
        UpdateChargeUi();
    }

    public void AddCharge(float charge)
    {
        _charge += charge;
        UpdateChargeUi();
        if (_charge >= _maxCharge)
        {
            
        }
    }

    private void UpdateChargeUi()
    {
        _chargeSlider.value = ToValue(_charge);
    }

    private float ToValue(float charge)
    {
        return charge / _maxCharge;
    }
}
