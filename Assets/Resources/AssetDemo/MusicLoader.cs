using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour {
    public AudioClip A1,A2,A3,A4,A5,C1,C2,Cin1;
    public AudioClip AmmoUsage, 
        AreaIntro, 
        BossRoomIntro, 
        Claymore, 
        Crossbow, 
        Door, 
        EnemySwordHit, 
        EngiRobotDeploy, 
        Grenade, 
        LeesGun, 
        Magnitizer, 
        MPPDD, 
        NailGun, 
        RapidFiringWeapon, 
        RobotWeapon, 
        RoboticEnemy, 
        Spaceship, 
        Transmission, 
        Windy,
        Crossbow2,
        LeesGun2,
        RoboticEnemy2,
        Windy2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadA1Music() {
        gameObject.GetComponent<AudioSource>().clip = A1;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadA2Music() {
        gameObject.GetComponent<AudioSource>().clip = A2;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadA3Music() {
        gameObject.GetComponent<AudioSource>().clip = A3;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadA4Music() {
        gameObject.GetComponent<AudioSource>().clip = A4;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadA5Music()
    {
        gameObject.GetComponent<AudioSource>().clip = A5;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void LoadC1Music() {
        gameObject.GetComponent<AudioSource>().clip = C1;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadC2Music()
    {
        gameObject.GetComponent<AudioSource>().clip = C2;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void LoadCinMusic()
    {
        gameObject.GetComponent<AudioSource>().clip = Cin1;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void LoadAmmoUsageSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = AmmoUsage;
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void LoadAreaIntroSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = AreaIntro;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadBossRoomIntroSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = BossRoomIntro;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadClaymoreSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Claymore;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadCrossbowSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Crossbow;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadDoorSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Door;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadEnemySwordHitSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = EnemySwordHit;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadEngiRobotDeploySFX()
    {
        gameObject.GetComponent<AudioSource>().clip = EngiRobotDeploy;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadGrenadeSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Grenade;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadLeesGunSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = LeesGun;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadMagnitizerSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Magnitizer;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadMPPDDSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = MPPDD;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadNailGunSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = NailGun;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadRapidFiringWeaponSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = RapidFiringWeapon;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadRobotWeaponSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = RobotWeapon;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadRoboticEnemySFX()
    {
        gameObject.GetComponent<AudioSource>().clip = RoboticEnemy;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadSpaceshipSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Spaceship;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadTransmissionSFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Transmission;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadWindySFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Windy;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadCrossbow2SFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Crossbow2;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadLeesGun2SFX()
    {
        gameObject.GetComponent<AudioSource>().clip = LeesGun2;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadRoboticEnemy2SFX()
    {
        gameObject.GetComponent<AudioSource>().clip = RoboticEnemy2;
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void LoadWindy2SFX()
    {
        gameObject.GetComponent<AudioSource>().clip = Windy2;
        gameObject.GetComponent<AudioSource>().Play();
    }


}
