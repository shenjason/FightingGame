using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShoot : MonoBehaviour
{
    public List<GameObject> Bullets;
    public FollowCam followCam;
    public Transform Firepoint;
    public Image ReloadIm;

    public float ReloadAmmoTime = 3;

    public int MaxAmmo = 30;

    public int Ammo;
    public CrossHairZoom cz;

    public float ReloadTime = 0.2f;
    public float AngleOffset = 0.5f;
    private float tick;
    private bool CanShoot;
    public ParticleSystem fx;

    void Awake()
    {
        tick = Mathf.Infinity;
        Ammo = MaxAmmo;
        CanShoot = true;
    }
    void Update()
    {
        if (!CanShoot) return;
        if (Input.GetKey(KeyCode.Mouse0) & tick > ReloadTime & Ammo > 0)
        {
            Shoot();
            cz.StartZoom();
            Ammo -= 1;
            ReloadIm.fillAmount = (float)Ammo/(float)MaxAmmo;
            tick = 0f;

        }
        tick += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot(){
        int Findi = FindIndex();
        BulletMove Bulletscript = Bullets[Findi].GetComponent<BulletMove>();
        float angle = followCam.angle + Random.Range(-AngleOffset, AngleOffset) * Mathf.Deg2Rad;
        Bullets[Findi].SetActive(true);
        Bulletscript.SpeedX = Mathf.Cos(angle);
        Bulletscript.SpeedY = Mathf.Sin(angle);
        Bulletscript.Setup(Firepoint.position, 0.5f);
        StartCoroutine(Recoil(0.2f, 0.05f));
        fx.Play();
    }

    int FindIndex(){
        for (int i = 0; i < Bullets.Count; i++){
            if (!Bullets[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
        }
    
    IEnumerator Reload()
    {
        CanShoot = false;
        int reload = Mathf.CeilToInt((ReloadAmmoTime - (ReloadAmmoTime/MaxAmmo * Ammo))/Time.deltaTime);
        print(reload);
        for (int i = 0; i<=reload; i++)
        {
            ReloadIm.fillAmount += 1/(float)reload;
            yield return new WaitForEndOfFrame();
        }
        Ammo = MaxAmmo;
        CanShoot = true;
    }

    IEnumerator Recoil(float Knock, float TimeBeforeReset)
    {
        Vector2 Og_pos = transform.localPosition;
        transform.localPosition = new Vector2(transform.localPosition.x - Knock * Mathf.Cos(followCam.angle), transform.localPosition.y - Knock * Mathf.Sin(followCam.angle));
        yield return new WaitForSeconds(TimeBeforeReset);
        transform.localPosition = Og_pos;
    }
}

