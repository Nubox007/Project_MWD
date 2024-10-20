using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Hualand : TowerManager
{
    private Dictionary<GameObject, Coroutine> dotDamageCoroutines = new Dictionary<GameObject, Coroutine>();
    private bool isMeteorActive = false; // ���׿� ȿ�� Ȱ��ȭ ����
    #region["Hualand �нú�: 3�� �� ��Ʈ ������"]
    public void DotDamage(GameObject _attackedenemy)
    {
        if (dotDamageCoroutines.ContainsKey(_attackedenemy))
        {
            StopCoroutine(dotDamageCoroutines[_attackedenemy]);
        }
        Coroutine coroutine = StartCoroutine(DotDamageCoroutine(_attackedenemy));
        dotDamageCoroutines[_attackedenemy] = coroutine;
    }
    #endregion
    #region["��Ʈ ������ �ڷ�ƾ"]
    private IEnumerator DotDamageCoroutine(GameObject _attackedenemy)
    {
        float continuetime = 0f;
        while (continuetime < 3f)
        {
            float damage = isMeteorActive ? 2f : 1f; // ���׿� Ȱ��ȭ �� 2�� ������, �׷��� ������ 1�� ������
            _attackedenemy.GetComponent<MonsterManager>().DamageByTower(damage);
            yield return new WaitForSeconds(1f);
            continuetime += 1f;
        }
        dotDamageCoroutines.Remove(_attackedenemy);
    }
    #endregion
    #region["���׿� ȿ�� ����"]
    public void SetMeteorActive(bool isActive)
    {
        isMeteorActive = isActive;
    }
    #endregion
    public void UpgradeAttackSpeed()
    {
        Debug.Log("���ݼӵ�2��");
        // ���� �ӵ��� 2��� ���׷��̵��մϴ�.
        AttackSpeed *= 0.5f;
    }
    public void ResetAttackSpeed()
    {
        Debug.Log("���ݼӵ� �ʱ�ȭ");
        // ���� �ӵ��� ������� �ʱ�ȭ�մϴ�.
        AttackSpeed = 2f;
    }
    #region["Active Skill -> Switch/Case �� ���"]
    protected virtual void Active(Vector3 _dir)
    {
        switch (name)
        {
            case "Hualand_PassionTower":
                // ȭ���� ����Ÿ��
                break;
            case "Hualand_HeroTower":
                // ȭ���� ����Ÿ��
                break;
            case "Hualand_PillaroffireTower":
                // ȭ���� �ұ�� Ÿ��
                break;
            case "Hualand_GoblinTower":
                // ȭ���� ������� Ÿ��
                break;
            case "Hualand_PenetrateTower":
                // ȭ���� ���� ��Ÿ��
                break;
            default:
                break;
        }
    }
    #endregion
    // ���� (ȭ���� Ÿ��)
    protected override void Update()
    {
        base.Update();
    }
}
