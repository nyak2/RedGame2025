using TMPro;
using UnityEngine;

public class ChargeKeeper : MonoBehaviour
{
    private static float _charge = 0.0f;
    [SerializeField] private TextMeshProUGUI _chargeTextMesh;
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
    }

    private void UpdateChargeUi()
    {
        _chargeTextMesh.text = _charge.ToString();
    }
}
