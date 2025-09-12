using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject m_weaponLogic;

    public void EnableWeapon()
    {
        m_weaponLogic.SetActive(true);
    }

    public void DisableWeapon()
    {
        m_weaponLogic.SetActive(false);
    }
}
