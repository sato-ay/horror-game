using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] Material changeMaterial;
    [SerializeField] Material changeMaterialForPicture;
    [SerializeField] GameObject[] monitorArray = new GameObject[3];
    [SerializeField] TVmusic tvMusic;
    private bool change;
    public static MaterialController Instance { get; private set; }

    private void Start() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MonitorChange() {
        if (!change)
        {
            change = !change;
            if (SoundManager.Instance.StopSE())
            {
                tvMusic.PlayingMusic(SESoundData.SE.TV2);
            }

            for (int i = 0; i < monitorArray.Length; i++)
            {
                if (monitorArray[i].name == "FloorPicture")
                {
                    monitorArray[i].GetComponent<MeshRenderer>().material = changeMaterialForPicture;
                }
                else
                {
                    monitorArray[i].GetComponent<MeshRenderer>().material = changeMaterial;
                }
            }
        }
    }
}
