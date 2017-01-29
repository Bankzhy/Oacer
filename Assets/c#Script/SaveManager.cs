using UnityEngine;
using System.Collections;

public static class SaveManager : object {
   static int crystals;
   static int level;
   static int bestdistance;

    public static void CreatOrLoad()
    {
        if (PlayerPrefs.HasKey("Crystals"))
        {
            LoadData();
        }
        else
        {
            CreateData();
        }
    }
    public static void CreateData()
    {
        Debug.Log("creat");
//        PlayerPrefs.SetInt("Crystals", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("MSpeedUp", 0);
        PlayerPrefs.SetInt("MTransparency", 0);
        PlayerPrefs.SetInt("BestDistance", 0);
        PlayerPrefs.Save();
    }

    public static void LoadData()
    {
        Debug.Log("load");
//        crystals = PlayerPrefs.GetInt("Crystals");
        level = PlayerPrefs.GetInt("Level");
        bestdistance = PlayerPrefs.GetInt("BestDistance");
    }

    public static void SetDistance(int distance) 
    {
        if (distance > bestdistance)
        {
            PlayerPrefs.SetInt("BestDistance", distance);
            PlayerPrefs.Save();
        }
    }
    public static void Castdistance(int distance)
    {
        PlayerPrefs.SetInt("BestDistance", distance);
        PlayerPrefs.Save();
    }
    //public static void SetCrystal(int crystal)
    //{
    //    int newcryCount = crystals + crystal;
    //    PlayerPrefs.SetInt("Crystals", newcryCount);
    //    PlayerPrefs.Save();
    //}
    public static int GetBestDistance()
    {
        Debug.Log(bestdistance);
        return bestdistance;

    }

}

