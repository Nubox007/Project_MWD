using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeManager : UIManager
{
    [SerializeField] private List<TowerManager> towerManagers = new List<TowerManager>(); // TowerManager ������Ʈ�� ������ ����Ʈ
    [SerializeField] private TowerManager tempTower = null;
    [SerializeField] private GameObject p_upgrade_ui = null;

    int num = 0;


    protected override void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� Ŭ���Ǿ����� Ȯ��
        {
            // ���콺 ��ġ�� �������� ���̸� ��� ���� ����� ������
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ����ĳ��Ʈ�� ���� �浹�� ����� ������
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Upgrade")))
            {
                Debug.Log("Upgrade");
                Debug.Log("hit.collider.name: " + hit.collider.name); 
                // �浹�� ����� Tower �±׸� ���� ������Ʈ���� Ȯ��
                if (hit.collider.CompareTag("Tower"))
                {
                    Debug.Log("Here?");
                    // �浹�� ������Ʈ�� TowerManager ������Ʈ�� ������ tempTower�� ����
                    p_upgrade_ui.SetActive(true);
                    Button yesbtn = p_upgrade_ui.GetComponentsInChildren<Button>()[0];
                    Button nobtn = p_upgrade_ui.GetComponentsInChildren<Button>()[1];
                    yesbtn.onClick.AddListener(
                        () => UpgradeTowerBtn(hit.collider.gameObject)
                    );
                    nobtn.onClick.AddListener(ExitBtn); 
                }
            }
        }
    }

    public void UpgradeTowerBtn(GameObject _hittedtower)
    {
        tempTower = _hittedtower.GetComponentInParent<TowerManager>();
        Debug.Log("temptower: " + tempTower.name);
        tempTower.EnableUpgradeRange();
        p_upgrade_ui.SetActive(false); 
    }

    public void ExitBtn()
    {
        p_upgrade_ui.SetActive(false); 
    }
    /*
    // �ݶ��̴��� ���� ���� �� ȣ���
    private void OnTriggerEnter(Collider other)
    {
        TowerManager towerManager = other.GetComponent<TowerManager>(); // ���� ��ü���� TowerManager ������Ʈ�� ������
        if (towerManager != null && !towerManagers.Contains(towerManager)) // ������ TowerManager�� ��ȿ�ϰ� ����Ʈ�� ������ �߰�
        {
            // ����Ʈ���� null�� �ִ� ���, �ش� �ε����� ���ο� ��Ҹ� �߰�
            int index = towerManagers.FindIndex(item => item == null);
            if (index != -1)
            {
                towerManagers[index] = towerManager;
            }
            else
            {
                towerManagers.Add(towerManager);
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
    */ 
}
