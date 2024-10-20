using EpicToonFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hualand_PassionTower : Hualand
{
    private int cooltime_PassionTower = 0;
    private float tempAttackSpeed;

    protected override void Awake()
    {
        tempAttackSpeed = AttackSpeed;
        base.Awake();
        towername = "HuaLand_PassionTower";
    }
    protected override void Start()
    {
        base.Start();
        base.GetTowerInfo(towername, ranked);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (!isAttack)
        {
            StartCoroutine(Attack(nextMonsterManager.transform.position));
            isAttack = true;
        }
    }

    private IEnumerator Attack(Vector3 targetPosition)
    {
        while (nextMonsterManager != null)
        {
            audiomanager.PlaySFX(0);
            GameObject projectile = Instantiate(bulletPrefab, spawn.transform.position, Quaternion.identity) as GameObject; // ������ �Ѿ��� ����
            MonsterMovement monsterMovement = nextMonsterManager.GetComponent<MonsterMovement>();
            projectile.transform.LookAt(nextMonsterManager.transform.position); // �Ѿ��� ȸ���� ���͸� ���ϵ��� ����
           
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * bulletSpeed); // ������ٵ� ���� ���Ͽ� �Ѿ� �ӵ� ����
            ETFXProjectileScript projectileScript = projectile.GetComponent<ETFXProjectileScript>();
            
            if (projectileScript != null)
            {                
                projectileScript.damage = AttackDamage; // ���� ��ũ��Ʈ�� attackDamage ������ ����
                DotDamage(nextMonsterManager.gameObject); 
                AttackSpeed *= 0.99f;
            }
            yield return new WaitForSeconds(AttackSpeed); // ���� �ӵ���ŭ ���
        }
        audiomanager.StopSFX(0); 
        isAttack = false;
        AttackSpeed = tempAttackSpeed;
        yield break;
    }



}
