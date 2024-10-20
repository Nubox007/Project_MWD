using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoSkill : MonoBehaviour
{
    [SerializeField] private List<MonsterManager> monsterGetDatas = new List<MonsterManager>();
    [SerializeField] private int damageAmount = 1; // �ѹ��� �� HP ��
    [SerializeField] private float damageInterval = 1f; // �������� �ִ� ���� (��)
    [SerializeField] private Transform startPoint; // ���� ����
    [SerializeField] private Transform endPoint; // ���� ����
    [SerializeField] private float speed = 1f; // �̵� �ӵ�
    [SerializeField] private float amplitude = 1f; // ���Ʒ� �������� ����
    [SerializeField] private float frequency = 1f; // ���Ʒ� �������� �ֱ�
    [SerializeField] private ParticleSystem tornadoParticleSystem; // ����̵� ��ƼŬ �ý���

    private float journeyLength; // ���� ������ ���� ���� ���� �Ÿ�
    private float startTime; // �̵��� ������ �ð�
    private bool isMoving = false; // ����̵��� �̵� ������ ����

    private void Start()
    {
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        //StartCoroutine(DamageMonstersOverTime());
    }

    private void Update()
    {
        if (!isMoving && tornadoParticleSystem.isPlaying) // ��ƼŬ �ý����� �۵� ���� ���� ������
        {
            MoveTornado();
        }
    }

    private void MoveTornado()
    {
        // ���� ������ ���� ���� ���̸� �̵��ϴ� ����
        StartCoroutine(MoveBetweenPoints());
    }

    private IEnumerator MoveBetweenPoints()
    {
        isMoving = true;
        float distanceCovered = 0f;
        startTime = Time.time;

        while (distanceCovered < journeyLength)
        {
            float fractionOfJourney = distanceCovered / journeyLength;
            Vector3 linearPosition = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);
            float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(linearPosition.x, linearPosition.y + yOffset, linearPosition.z);

            // �̵��� �Ÿ� ����
            distanceCovered = (Time.time - startTime) * speed;
            yield return null;
        }

        // �̵��� ������ ��ƼŬ �ý��� ���� �� ��Ȱ��ȭ
        tornadoParticleSystem.Stop();
        tornadoParticleSystem.Clear();
        isMoving = false;
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            collider.GetComponent<MonsterManager>().GetComponent<MonsterManager>().DamageByTower(damageAmount);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterManager monsterGetData = collider.gameObject.GetComponent<MonsterManager>();
            if (monsterGetData != null && monsterGetDatas.Contains(monsterGetData))
            {
                monsterGetDatas.Remove(monsterGetData);
            }
        }
    }

    private IEnumerator DamageMonstersOverTime()
    {
        while (true)
        {
            for (int i = 0; i < monsterGetDatas.Count; i++)
            {
                if (monsterGetDatas[i] != null)
                {
                    monsterGetDatas[i].DamageByTower(damageAmount);
                }
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
