using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealthSystem : MonoBehaviourPunCallbacks
{
    [SerializeField] private HealthControler _healthController;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage();
        }
    }

    
    void Damage()
    {
        _healthController.playerHealth = _healthController.playerHealth - 1;
        _healthController.UpdateHealth();

    }
}
