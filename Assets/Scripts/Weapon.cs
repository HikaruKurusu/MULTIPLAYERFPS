using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun;
using TMPro;
public class Weapon : MonoBehaviour
{
    public int damage;

    public Camera camera;
    public float fireRate;

    private float nextFire;

    [Header("VFX")]
    public GameObject hitVFX;

    [Header("Ammo")]
    public int mag = 5;

    public int ammo = 30;

    public int magAmmo = 30;
    [Header("UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;

    [Header("Animation")]
    public Animation animation;
    public AnimationClip reload;

    // Update is called once per frame
    void Start() {
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
    }
    void Update()
    {
        if(nextFire > 0) {
            nextFire -= Time.deltaTime;
        }
        if(Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && animation.isPlaying == false) {
            nextFire = 1 / fireRate;
            ammo--;
            magText.text = mag.ToString();
            ammoText.text = ammo + "/" + magAmmo;
            Fire();
            
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }
    }
    void Reload() {
        animation.Play(reload.name);
        if(mag > 0) {
            mag--;
            ammo = magAmmo;
        }
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
    }
    void Fire() {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 100f)) {
            PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);
            if(hit.transform.gameObject.GetComponent<Health>()) {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }

    }
}
