using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PunObj : MonoBehaviourPunCallbacks
{
    public delegate void OnPunCallBackMethod(Vector2Int vector2);
    private OnPunCallBackMethod onPunCallBackMethod = null;
    // private OnPunCallBackMethod onPunCallBackColider = null;

    public void SetOnPunCallBackMethod(OnPunCallBackMethod _OnPunCallBackMethod)
    {

        onPunCallBackMethod = _OnPunCallBackMethod;
    }


    [PunRPC]
    private void SendPhotonRpc(int _x, int _y)
    {
        Vector2Int curNum = new Vector2Int(_x, _y);
     
        // Debug.LogError(onPunCallBackMethod != null ? "True" :"False");
        onPunCallBackMethod?.Invoke(curNum);
    }
    public void CallBackPun(Vector2Int _curNum)
    {
        photonView.RPC("SendPhotonRpc", RpcTarget.All, _curNum.x, _curNum.y);
    }

}
