using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun.Demo.PunBasics;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;
    public GameObject camera;

    public void IsLocalPlayer() {
        movement.enabled = true;
        camera.SetActive(true);
    }
}
