  using System.Collections;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Diagnostics;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet; 
    [SerializeField] 
    private Transform BulletDirection;
    private Transform aimTransform;
    private Camera main;
    private bool canShoot = true;
    [SerializeField] private float coolDownTime = 0.5f;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        main = Camera.main;
    }

    private void Update()
    {
        HandleAiming();
        if (Input.GetMouseButtonDown(0))
        {
            PlayerShoot();
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //float adjustedAngle = angle - 90f; 
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

                //To keep weapon from going upside down when facing left 
        Vector3 aimLocalScale = Vector3.one; 
        if (angle > 90 || angle < -90) { 
            aimLocalScale.y = -1f; 
        } else { 
            aimLocalScale.y = +1f; 
        }
        aimTransform.localScale = aimLocalScale; 
    }

    private void PlayerShoot()
    {
        if (!canShoot)
        {
            return;
        } 
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = (Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg) - 90f;
        GameObject g = Instantiate(bullet, BulletDirection.position, Quaternion.Euler(0, 0, angle)); //aimTransform second, BulletDirection.positon 
        g.SetActive(true);
        StartCoroutine(CanShoot());
    }

    private IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(coolDownTime);
        canShoot = true;
    }
}

