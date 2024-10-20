using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeManager : UIManager
{
    [SerializeField] private List<TowerManager> towerManagers = new List<TowerManager>(); // TowerManager 오브젝트를 저장할 리스트
    [SerializeField] private TowerManager tempTower = null;
    [SerializeField] private GameObject p_upgrade_ui = null;

    int num = 0;


    protected override void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼이 클릭되었는지 확인
        {
            // 마우스 위치를 기준으로 레이를 쏘아 맞은 대상을 가져옴
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 레이캐스트를 통해 충돌한 대상을 가져옴
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Upgrade")))
            {
                Debug.Log("Upgrade");
                Debug.Log("hit.collider.name: " + hit.collider.name); 
                // 충돌한 대상이 Tower 태그를 가진 오브젝트인지 확인
                if (hit.collider.CompareTag("Tower"))
                {
                    Debug.Log("Here?");
                    // 충돌한 오브젝트의 TowerManager 컴포넌트를 가져와 tempTower에 저장
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
    // 콜라이더에 무언가 들어올 때 호출됨
    private void OnTriggerEnter(Collider other)
    {
        TowerManager towerManager = other.GetComponent<TowerManager>(); // 들어온 객체에서 TowerManager 컴포넌트를 가져옴
        if (towerManager != null && !towerManagers.Contains(towerManager)) // 가져온 TowerManager가 유효하고 리스트에 없으면 추가
        {
            // 리스트에서 null이 있는 경우, 해당 인덱스에 새로운 요소를 추가
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

    // 콜라이더에서 무언가 나갈 때 호출됨
    private void OnTriggerExit(Collider other)
    {
        TowerManager towerManager = other.GetComponent<TowerManager>(); // 나간 객체에서 TowerManager 컴포넌트를 가져옴
        if (towerManager != null && towerManagers.Contains(towerManager)) // 가져온 TowerManager가 유효하고 리스트에 있으면 삭제
        {
            towerManagers.Remove(towerManager); // 리스트에서 제거
        }
    }
    */ 
}
