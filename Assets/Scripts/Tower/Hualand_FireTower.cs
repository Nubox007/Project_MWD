using EpicToonFX;
using System.Collections;
using UnityEngine;

public class Hualand_FireTower : Hualand
{
    protected override void Awake()
    {
        towername = "HuaLand_FireTower";
    }
    
    protected override void Start()
    {
        base.Start(); 
        base.GetTowerInfo(towername, ranked);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if( other.gameObject.CompareTag("Monster") && towername == "HuaLand_FireTower") { 
            if (!isAttack)
            {
                StartCoroutine(Attack(nextMonsterManager.transform.position));
                isAttack = true;
            }
        }
    }

    private IEnumerator Attack(Vector3 targetPosition)
    {
        while (nextMonsterManager != null)
        {
            audiomanager.PlaySFX(0); 
            GameObject projectile = Instantiate(bulletPrefab, spawn.transform.position, Quaternion.identity) as GameObject; // ������ �Ѿ��� ����
            projectile.transform.LookAt(nextMonsterManager.transform.position); // �Ѿ��� ȸ���� ���͸� ���ϵ��� ����
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * bulletSpeed); // ������ٵ� ���� ���Ͽ� �Ѿ� �ӵ� ����
            ETFXProjectileScript projectileScript = projectile.GetComponent<ETFXProjectileScript>();
            if (projectileScript != null)
            {
                projectileScript.damage = AttackDamage; // ���� ��ũ��Ʈ�� attackDamage ������ ����
                DotDamage(nextMonsterManager.gameObject);
            }
            yield return new WaitForSeconds(AttackSpeed); // ���� �ӵ���ŭ ���
        }
        audiomanager.StopSFX(0); 
        isAttack = false;
        yield break;
    }

}
