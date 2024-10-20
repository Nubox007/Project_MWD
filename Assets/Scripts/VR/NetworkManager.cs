using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public ParticleSystem meteorParticleSystem;
    public ParticleSystem tornadoParticleSystem;
    public ParticleSystem[] waterParticleSystem; // �߰��� ���� ��ƼŬ �ý���

    private void Start()
    {
        ToConnectServer();
        
    }

    private void ToConnectServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        //Debug.Log("Trying To Connecting Server....");
    }

    public override void OnConnectedToMaster()
    {
        //Debug.Log("Connected To Server...");
        base.OnConnectedToMaster();
        RoomOptions room = new RoomOptions();

        room.MaxPlayers = 2;
        room.IsVisible = true;
        room.IsOpen = true;
        room.BroadcastPropsChangeToAll = true;
        PhotonNetwork.JoinOrCreateRoom("testRoom", room, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined a room...");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player _newPlayer)
    {
        Debug.Log("A player Joined the room...");
        base.OnPlayerEnteredRoom(_newPlayer);
    }

    public void MeteorStart()
    {
        Debug.Log("Meteor Start");
        photonView.RPC("StartParticleSystem", RpcTarget.All, "Meteor");
    }

    public void TornadoStart()
    {
        Debug.Log("Tornado Start");
        photonView.RPC("StartParticleSystem", RpcTarget.All, "Tornado");
    }

    public void WaterStart() // ���� ��ƼŬ �ý����� �����ϱ� ���� �޼ҵ�
    {
        Debug.Log("Water Start");
        photonView.RPC("StartParticleSystem", RpcTarget.All, "Water");
    }

    public void TriggerStop()
    {
        photonView.RPC("StopAllParticleSystems", RpcTarget.All);
    }

    [PunRPC]
    public void StartParticleSystem(string particleSystemType)
    {
        Debug.Log("Start Particle System: " + particleSystemType);

        if (particleSystemType == "Meteor" && meteorParticleSystem != null)
        {
            meteorParticleSystem.Play();
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

    [PunRPC]
    public void StopAllParticleSystems()
    {
        Debug.Log("Stop All Particle Systems");

        if (meteorParticleSystem != null)
        {
            meteorParticleSystem.Stop();
        }

        if (tornadoParticleSystem != null)
        {
            tornadoParticleSystem.Stop();
        }

        if (waterParticleSystem != null) // ���� ��ƼŬ �ý��� ����
        {
            for (int i = 0; i < waterParticleSystem.Length; i++)
            {
                waterParticleSystem[i].Play();
            }
        }
    }
}
