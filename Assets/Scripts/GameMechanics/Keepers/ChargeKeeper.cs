using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChargeKeeper : MonoBehaviour
{
    private static float _charge = 0.0f;
    private static readonly float _maxCharge = 100.0f;
    [SerializeField] private Slider _chargeSlider;
    [SerializeField] private SpriteRenderer _chargeIcon;
    [SerializeField] private SkillTrigger _skillTrigger;
    private SFXPlayer _sfxPlayer;
    public static ChargeKeeper Instance;
    private List<KeyValuePair<float, Sprite>> _increasingChargeLevelsToSprites;

    private void Awake()
    {
        Instance = this;
        _sfxPlayer = GetComponent<SFXPlayer>();

        Dictionary<string, Sprite> biggieChargeIcons = Resources.LoadAll<Sprite>("UI/Biggie_ChargeIcon")
            .ToDictionary(sprite => sprite.name, sprite => sprite);
        _increasingChargeLevelsToSprites = new()
        {
            new(0.0f, biggieChargeIcons["Biggie_ChargeIcon_0"]),
            new(30.0f, biggieChargeIcons["Biggie_ChargeIcon_1"]),
            new(60.0f, biggieChargeIcons["Biggie_ChargeIcon_2"]),
            new(90.0f, biggieChargeIcons["Biggie_ChargeIcon_3"])
        };

        UpdateChargeUi();
    }

    public void AddCharge(float charge)
    {
        _charge += charge;
        if (_charge >= _maxCharge)
        {
            _skillTrigger.TriggerRandomSkill();
            _charge = 0.0f;
        }
        UpdateChargeUi();
    }

    private void UpdateChargeUi()
    {
        _chargeSlider.value = ToValue(_charge);
        Sprite prevIcon = _chargeIcon.sprite;
        Sprite nextIcon = GetChargeLevelIcon(_charge);
        if (prevIcon != nextIcon)
        {
            _chargeIcon.sprite = nextIcon;
            _sfxPlayer.PlaySfx(SFXLibrary.SFX_NEW_CHARGE_LEVEL);
        }
    }

    private float ToValue(float charge)
    {
        return charge / _maxCharge;
    }

    private Sprite GetChargeLevelIcon(float charge)
    {
        Sprite result = null;
        foreach (var pair in _increasingChargeLevelsToSprites)
        {
            if (charge >= pair.Key)
            {
                result = pair.Value;
                continue;
            }
            break;
        }
        return result;
    }
}
