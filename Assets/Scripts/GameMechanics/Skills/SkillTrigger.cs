using System.Collections.Generic;
using UnityEngine;

public class SkillTrigger : MonoBehaviour
{
    private List<ISkill> _skills;
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        _skills = new()
        {
            gameObject.AddComponent<ExplosionSkill>()
        };
    }

    public void TriggerRandomSkill()
    {
        if (_skills.Count == 0)
        {
            return;
        }

        int randIdx = Random.Range(0, _skills.Count);
        ISkill skill = _skills[randIdx];
        TriggerSkill(skill);
    }

    public void TriggerSkill(ISkill skill)
    {
        _gameManager.CanControlControllers(false);
        skill.Activate();
        _gameManager.CanControlControllers(true);
    }
}
