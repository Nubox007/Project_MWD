using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//2024-05-22: CUSTOM UNITY TEMPLATE 

public class TowerUpgradeRangeManager : MonoBehaviour
{
    private List<TowerManager> towerManagers = new List<TowerManager>(); // TowerManager ������Ʈ�� ������ ����Ʈ
    public GridSystem gridSystem;
    [SerializeField] private string towername = string.Empty;
    Collider colliders;
    private void Awake()
    {
        colliders = GetComponent<Collider>();
        GameObject Grid = GameObject.FindGameObjectWithTag("GridSystem");
        if (Grid != null)
        {
            gridSystem = Grid.GetComponent<GridSystem>();
        }       
    }
    // �ݶ��̴��� ���� ���� �� ȣ���
    private void OnTriggerEnter(Collider other)
    {
        if(transform.parent.gameObject.GetComponent<TowerManager>().GetRanked() == 6)
        {
            Debug.Log("�̹� �ְ��� �Դϴ�.");
            return; 
        }
        if(other.transform.parent != null && other.transform.parent.name.Contains(transform.parent.name) && other.transform.parent.CompareTag("Tower"))
        {
            Debug.Log("Maybe..."); 
            if(other.transform.parent.gameObject.GetComponent<TowerManager>().GetRanked() == transform.parent.gameObject.GetComponent<TowerManager>().GetRanked())
            {
                Debug.Log("Here!"); 
                TowerManager towerManager = other.transform.parent.gameObject.GetComponent<TowerManager>(); // ���� ��ü���� TowerManager ������Ʈ�� ������
                if (towerManager != null && !towerManagers.Contains(towerManager)) // ������ TowerManager�� ��ȿ�ϰ� ����Ʈ�� ������ �߰�
                {
                    // ����Ʈ���� null�� �ִ� ���, �ش� �ε����� ���ο� ��Ҹ� �߰�
                    int index = towerManagers.FindIndex(item => item == null);
                    if (index != -1)
                    {
                        towerManagers[index] = towerManager;
                        StartCoroutine(UpgradeTower(towerManager));

                    }
                    else
                    {
                        towerManagers.Add(towerManager);
                        
                        StartCoroutine(UpgradeTower(towerManager));
                    }
                }
            }
        }
    }

    // �ݶ��̴����� ���� ���� �� ȣ���
    private void OnTriggerExit(Collider other)
    {
        TowerManager towerManager = other.GetComponent<TowerManager>(); // ���� ��ü���� TowerManager ������Ʈ�� ������
        if (towerManager != null && towerManagers.Contains(towerManager)) // ������ TowerManager�� ��ȿ�ϰ� ����Ʈ�� ������ ����
        {
            towerManagers.Remove(towerManager); // ����Ʈ���� ����
        }
    }


    private IEnumerator UpgradeTower(TowerManager _tower)
    {
        if (_tower != null)
        {
            /*
            _tower.TowerUpgrade(towername);
            Destroy(transform.parent.transform.parent.gameObject); 
            */

            //�ڱ� �ڽ��� ���׷��̵� 

            transform.parent.gameObject.GetComponent<TowerManager>().TowerUpgrade(towername);
            colliders.enabled = false;
            //������ Ÿ���� ���� 
            /* Node aa = gridSystem.WorldToGridNode(_tower.transform.parent.gameObject.transform.position);
             * 
             aa.SetTileStatus(placementStatus.Filled);*/
            gridSystem.SetTile(_tower.transform.parent.gameObject.transform.position, placementStatus.Filled);


            yield return new WaitForSeconds(0.2f);
            Destroy(_tower.transform.parent.gameObject);
            yield return null;
        }
        yield break;
    }
}

