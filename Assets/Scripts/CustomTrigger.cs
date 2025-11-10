using UnityEngine;
using System.Collections.Generic;

public class CustomTrigger : MonoBehaviour
{
    [SerializeField] private MonoBehaviour receiver;
    private HashSet<Collider> _currentTriggers = new HashSet<Collider>();
    private ICustomTriggerReceiver _receiver;

    void Awake() {
        if (receiver != null)
            _receiver = receiver as ICustomTriggerReceiver;
    }

    void Update()
    {
        CustomTriggerCheck();
    }

    private void CustomTriggerCheck()
    {
        _currentTriggers.RemoveWhere(c => c == null);

        Collider[] hits = Physics.OverlapCapsule(transform.position + new Vector3(0, 0.5f, 0), transform.position - new Vector3(0, 0.5f, 0), 0.5f);
        // Detect enters
        foreach (var hit in hits)
        {
            if (!hit.isTrigger)
                continue;

            if (_currentTriggers.Add(hit))
            {
                _receiver?.OnCustomTriggerEnter(hit);
                //OnCustomTriggerEnter(hit);
            }
            else
            {
                // Was already in set → staying
                _receiver?.OnCustomTriggerStay(hit);
                //OnCustomTriggerStay(hit);
            }
        }

        // Detect exits
        _currentTriggers.RemoveWhere(c =>
        {
            if (!System.Array.Exists(hits, h => h == c))
            {
                _receiver?.OnCustomTriggerExit(c);
                //OnCustomTriggerExit(c);
                return true;
            }
            return false;
        });
    }

    
}
