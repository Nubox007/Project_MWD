using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    private PhotonView photonView = null;
    private Camera cam = null;
    bool triggerValue;
    public ParticleManager networkManager = null;
    [SerializeField] private float upgradeRange = 1f;
    private bool isInRange = false; // ��Ʈ��ũ �÷��̾ ���� �ȿ� �ִ��� ����

    private Hualand[] allTowers;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

        // ��Ʈ��ũ �Ŵ����� ã�Ƽ� �Ҵ�
        networkManager = FindObjectOfType<ParticleManager>();

        // ���� ��� Hualand Ÿ���� ã���ϴ�.
        allTowers = FindObjectsOfType<Hualand>();
    }

    public void SetCamera(Camera _targetCam)
    {
        cam = _targetCam;
        _targetCam.transform.SetParent(head);
        XROrigin xROrigin = GetComponent<XROrigin>();
        xROrigin.Camera = cam;
        _targetCam.transform.position = head.position;
        _targetCam.transform.rotation = head.rotation;
    }

    private void Update()
    {
        MapPosition(head, XRNode.Head);
    }

    private void MapPosition(Transform _target, XRNode _node)
    {
        if (!photonView.IsMine) return;
        InputDevices.GetDeviceAtXRNode(_node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 _position);
        InputDevices.GetDeviceAtXRNode(_node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion _rotation);

        _target.localPosition = _position;
        _target.localRotation = _rotation;
    }

    public void MeteorOn()
    {
        Debug.Log("���׿�");
        networkManager.MeteorStart();
    }

    public void TornadoOn()
    {
        Debug.Log("����̵�");
        gameObject.SetActive(true);
        networkManager.TornadoStart();
    }

    public void WaterOn()
    {
        Debug.Log("����");
        networkManager.WaterStart();
    }

   /* private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        // ��Ʈ��ũ �÷��̾ ���� �ȿ� ������ �� ȣ��˴ϴ�.
        if (other.CompareTag("Tower"))
        {
            Debug.Log("�ݶ��̴���");
            isInRange = true;
            UpgradeTowers();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        // ��Ʈ��ũ �÷��̾ ������ ����� �� ȣ��˴ϴ�.
        if (other.CompareTag("Tower"))
        {
            isInRange = false;
            ResetTowers();
        }
    }

    private void UpgradeTowers()
    {
        if (isInRange)
        {
            Debug.Log("Ÿ��ã��");
            foreach (var tower in allTowers)
            {
                if (Vector3.Distance(tower.transform.position, transform.position) <= upgradeRange)
                {
                    Debug.Log("Ÿ�� ���׷��̵�");
                    tower.UpgradeAttackSpeed();
                }
            }
        }
    }

    private void ResetTowers()
    {
        foreach (var tower in allTowers)
        {
            if (Vector3.Distance(tower.transform.position, transform.position) <= upgradeRange)
            {
                tower.ResetAttackSpeed();
            }
        }
    }

    [PunRPC]
    private void SetInRange(bool inRange)
    {
        isInRange = inRange;
        if (isInRange)
        {
            UpgradeTowers();
        }
        else
        {
            ResetTowers();
        }
    }*/
}
