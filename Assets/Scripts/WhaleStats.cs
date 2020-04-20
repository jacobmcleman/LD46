using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleStats : MonoBehaviour
{
    public int HealthPoolLevel;
    public int RegenLevel;
    public int DecoyLevel;

    public int ArmorLevel;
    public int TurretsLevel;
    public int TeslaLevel;

    public int MaxLevel;

    public int Organics;
    public int Mechanicals;

    public static WhaleStats instance;

    public List<int> Waves = new List<int>();

    public enum Upgrade
    {
        HealthPool,
        Regen,
        Decoy,
        Armor,
        Turrets,
        Tesla
    }

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); }
        else
        {
            instance = this;
            Organics = 0;
            Mechanicals = 0;
            DontDestroyOnLoad(this.gameObject);
            if (GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().Waves != null)
            {
                Waves = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().Waves;
            }
            else
            {
                Waves.Add(3);
            }
        }
    }

    public bool UpgradeLevel (string up)
    {
        Debug.Log(up);
        switch (up)
        {
            case "HealthPool":
                if (HealthPoolLevel + 1 > MaxLevel) { return false; }
                else
                {
                    HealthPoolLevel++;
                    return true;
                }
            case "Regen":
                if (RegenLevel + 1 > MaxLevel) { return false; }
                else
                {
                    RegenLevel++;
                    return true;
                }
            case "Decoy":
                if (DecoyLevel + 1 > MaxLevel) { return false; }
                else
                {
                    DecoyLevel++;
                    return true;
                }
            case "Armor":
                if (ArmorLevel + 1 > MaxLevel) { return false; }
                else
                {
                    ArmorLevel++;
                    return true;
                }
            case "Turrets":
                if (TurretsLevel + 1 > MaxLevel) { return false; }
                else
                {
                    TurretsLevel++;
                    return true;
                }
            case "Tesla":
                if (TeslaLevel + 1 > MaxLevel) { return false; }
                else
                {
                    TeslaLevel++;
                    return true;
                }
            default:
                return false;
        }
    }
}