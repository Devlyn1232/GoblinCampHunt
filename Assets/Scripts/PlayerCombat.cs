using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool CanAttack;
    public GameObject Cam;
    /*
    [Header("Main Hand Weapons")]
    [SerializeField] private GameObject MainWeaponSelected;
    [Tooltip("0 = Sword, 1 = Axe, 2 = Hammer, 3 = Dagger    Select Before Play")]
    public int MainWeaponSelection;
    public GameObject Sword;
    public float SwordDamage;
    public GameObject Axe;
    public float AxeDamage;
    public GameObject Hammer;
    public float HammerDamage;
    public GameObject Dagger1;
    public float Dagger1Damage;
    private float WeaponDamage;
    [Header("Off Hand Weapons")]
    [SerializeField] private GameObject SideWeaponSelected;
    [Tooltip("0 = Bow, 1 = Sheid, 2 = Dagger                Select Before Play")]
    public int SideWeaponSelection;
    public GameObject Bow;
    public GameObject Sheid;
    public GameObject Dagger2;
    */
    public GameObject[] mainWeapons; // Array containing prefabs of main weapons (Sword, Axe, Hammer, etc.)
    public float[] mainWeaponDamage;
    public float[] mainWeaponAttackCooldown;
    public int mainWeaponSelection = 0; // Set this in the Inspector or code to select the main weapon
    public GameObject[] sideWeapons; // Array containing prefabs of side weapons (Bow, Sheid, Dagger2, etc.)

    public int sideWeaponSelection = 0; // Set this in the Inspector or code to select the side weapon

    
    

    [Header("BlockingStuf")]
    public bool Blocking;
    public float BlockGainPerTick = .5f; //a Tick = a second
    public float MaxBlockIntegredy = 100;
    public float BlockIntegredy;
    [HideInInspector] public bool GainMoreBlock = true;

    // Start is called before the first frame update
    void Start()
    {/*
        
        switch (MainWeaponSelection) {
            case 0:
                MainWeaponSelected = Sword;
                WeaponDamage = SwordDamage;
                break;
            case 1:
                MainWeaponSelected = Axe;
                WeaponDamage = AxeDamage;
                break;
            case 2:
                MainWeaponSelected = Hammer;
                WeaponDamage = HammerDamage;
                break;
            case 3:
                MainWeaponSelected = Dagger1;
                WeaponDamage = Dagger1Damage;
                break;
            default:
                MainWeaponSelected = Dagger1;
                WeaponDamage = Dagger1Damage;
                break;
            }

            switch (SideWeaponSelection) {
            case 0:
                SideWeaponSelected = Bow;
                break;
            case 1:
                SideWeaponSelected = Sheid;
                break;
            case 2:
                SideWeaponSelected = Dagger2;
                break;
            default:
                SideWeaponSelected = Dagger2;
                break;
            }
        GameObject mainWeaponInstance = Instantiate(MainWeaponSelected, transform.position, transform.rotation);
        GameObject sideWeaponInstance = Instantiate(SideWeaponSelected, transform.position, transform.rotation);
        MeleeScript meleeScript = MainWeaponSelected.GetComponent<MeleeAttack>();

        // Set the instantiated weapons as children of the object that called them
        mainWeaponInstance.transform.parent = Cam.transform;
        sideWeaponInstance.transform.parent = Cam.transform;
        */
        GameObject mainWeaponPrefab = mainWeapons[mainWeaponSelection];
        GameObject sideWeaponPrefab = sideWeapons[sideWeaponSelection];

        GameObject mainWeaponInstance = Instantiate(mainWeaponPrefab, transform.position, transform.rotation);
        GameObject sideWeaponInstance = Instantiate(sideWeaponPrefab, transform.position, transform.rotation);

        MeleeAttack meleeScript = mainWeaponInstance.GetComponent<MeleeAttack>();

        float weaponDamage = mainWeaponDamage[mainWeaponSelection];
        meleeScript.Damage = weaponDamage;
        float weaponCooldown = mainWeaponAttackCooldown[mainWeaponSelection];
        meleeScript.attackCooldown = weaponCooldown;
        
        // Set the instantiated weapons as children of the object that called them
        mainWeaponInstance.transform.parent = Cam.transform;
        sideWeaponInstance.transform.parent = Cam.transform;

        BlockIntegredy = MaxBlockIntegredy;
    }

    // Update is called once per frame
    void Update()
    {
        if (BlockIntegredy > 100 && GainMoreBlock)
            StartCoroutine(RegainBlock());
    }
    public IEnumerator RegainBlock()
    { 
        GainMoreBlock = false;
        BlockIntegredy += BlockGainPerTick;
        yield return new WaitForSeconds(1f);
        GainMoreBlock = true;
    }
}
