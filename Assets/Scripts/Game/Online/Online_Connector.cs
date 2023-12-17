using UnityEngine;
using FishNet.Object;

public class Online_Connector : NetworkBehaviour
{
    private Transform Main_Canvas;
    private Transform Chosen_Player_Canvas;
    private GameObject _panel;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            Main_Canvas = transform.Find("Canvas");
            Chosen_Player_Canvas = Main_Canvas.Find("Chosen_Player_Canvas");
            _panel = Chosen_Player_Canvas.gameObject;
        }
        else
        {
            GetComponent<Online_Connector>().enabled = false;
        }
    }

    public void EnableFX_UI()
    {
        _panel.SetActive(!_panel.activeSelf);
    }
}
