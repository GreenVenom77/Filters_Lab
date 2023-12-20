using UnityEngine;
using FishNet.Object;

public class Online_Connector : NetworkBehaviour
{
    public GameObject _panel;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {

        }
    }

    [ObserversRpc]
    public void EnableFX_UI()
    {
        _panel.SetActive(true);
    }

    [ObserversRpc]
    public void DisableFX_UI()
    {
        _panel.SetActive(false);
    }
}
