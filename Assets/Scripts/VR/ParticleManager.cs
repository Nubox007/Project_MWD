using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class ParticleManager : MonoBehaviourPunCallbacks
{
    public ParticleSystem[] meteorParticleSystem;
    public ParticleSystem tornadoParticleSystem;
    public ParticleSystem[] waterParticleSystem; // �߰��� ���� ��ƼŬ �ý���

    private bool isCasting = false;
    private WaveManager waveManager;

    private void Start()
    {
        waveManager = FindObjectOfType<WaveManager>();
    }

    public void MeteorStart()
    {
        if (waveManager != null && waveManager.CanUseMeteor())
        {
            photonView.RPC("StartParticleSystem", RpcTarget.All, "Meteor");
        }

        // ��� Hualand �ν��Ͻ��� ���׿� �̺�Ʈ �˸�
        Hualand[] hualandTowers = FindObjectsOfType<Hualand>();
        foreach (var tower in hualandTowers)
        {
            tower.SetMeteorActive(true);
        }

        // ���� �ð� �� ���׿� ȿ�� ����
        StartCoroutine(ResetMeteorEffect());
    }

    private IEnumerator ResetMeteorEffect()
    {
        yield return new WaitForSeconds(10f);

        // ���׿� ȿ�� ����
        Hualand[] hualandTowers = FindObjectsOfType<Hualand>();
        foreach (var tower in hualandTowers)
        {
            tower.SetMeteorActive(false);
        }
    }

    public void TornadoStart()
    {
        if (waveManager != null && waveManager.CanUseTornado())
        {
            photonView.RPC("StartParticleSystem", RpcTarget.All, "Tornado");
        }
    }

    public void WaterStart() // ���� ��ƼŬ �ý����� �����ϱ� ���� �޼ҵ�
    {
         if (waveManager != null && waveManager.CanUseWater())
         {
        photonView.RPC("StartParticleSystem", RpcTarget.All, "Water");
         }

    }

/*    public void TriggerStop()
    {
        photonView.RPC("StopAllParticleSystems", RpcTarget.All);
    }
*/
    [PunRPC]
    public void StartParticleSystem(string particleSystemType)
    {
        
        //Debug.Log("Start Particle System: " + particleSystemType);

        if (particleSystemType == "Meteor" && meteorParticleSystem != null)
        {
            for (int i = 0; i < meteorParticleSystem.Length; i++)
            {
                meteorParticleSystem[i].Play();
            }
        }
        else if (particleSystemType == "Tornado" && tornadoParticleSystem != null)
        {
            tornadoParticleSystem.Play();
        }
        else if (particleSystemType == "Water" && waterParticleSystem != null) // �߰��� ��ƼŬ �ý��� ����
        {
            for (int i = 0; i < waterParticleSystem.Length; i++)
            {
                waterParticleSystem[i].Play();
            }
        }

    }

    

    /*[PunRPC]
    public void StopAllParticleSystems()
    {
        Debug.Log("Stop All Particle Systems");

        if (meteorParticleSystem != null)
        {
            for (int i = 0; i < meteorParticleSystem.Length; i++)
            {
                meteorParticleSystem[i].Stop();
            }
        }

        if (tornadoParticleSystem != null)
        {
            tornadoParticleSystem.Stop();
        }

        if (waterParticleSystem != null) // ���� ��ƼŬ �ý��� ����
        {
            for (int i = 0; i < waterParticleSystem.Length; i++)
            {
                waterParticleSystem[i].Stop();
            }
        }
    }*/
}
