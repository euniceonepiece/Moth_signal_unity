using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAudio : MonoBehaviour {

    public AudioClip OpenDoorSound;  //指定需要播放的音效
    private AudioSource source;   //必须定义AudioSource才能调用AudioClip

    private void OnTriggerEnter(Collider other)
    {
      if(other.name == "Cube")
      {
          source.PlayOneShot(OpenDoorSound);
      }
    }

    void Start () {
        source = GetComponent<AudioSource>();  //将this Object 上面的Component赋值给定义的AudioSource
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}