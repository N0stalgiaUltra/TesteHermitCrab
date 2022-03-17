using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AddsManager : MonoBehaviour
{
#if UNITY_ANDROID
    private string gameId = "4656545";
#else
    private string gameId = "4656544";
#endif

    public static AddsManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        Advertisement.Initialize(gameId);
    }

    public void PlayAd()
    {
        if (Advertisement.IsReady("add"))
        {
            Advertisement.Show("add");
        }
    }
}