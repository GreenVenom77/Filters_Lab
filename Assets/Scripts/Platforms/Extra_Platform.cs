using UnityEngine;

public class Extra_Platform : MonoBehaviour
{
    [SerializeField] private Filters_Control FControl;
    private bool _enabled = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FControl.ToggleOn(_enabled);
            _enabled = !_enabled;
        }
    }
}
