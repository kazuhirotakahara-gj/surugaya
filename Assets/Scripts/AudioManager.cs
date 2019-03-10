using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                Type t = typeof(T);

                _Instance = (T)FindObjectOfType(t);
                if (_Instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return _Instance;
        }
    }

    virtual protected void Awake()
    {
        // 他のGameObjectにアタッチされているか調べる.
        // アタッチされている場合は破棄する.
        if (this != Instance)
        {
            Destroy(this);
            //Destroy(this.gameObject);
            Debug.LogError(
                typeof(T) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            return;
        }

        // なんとかManager的なSceneを跨いでこのGameObjectを有効にしたい場合は
        // ↓コメントアウト外してください.
        //DontDestroyOnLoad(this.gameObject);
    }

}

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    public AudioClip Countdown;

    public AudioClip GameStart;

    public AudioClip GameEnd;

    public AudioClip ItemPick;

    public AudioClip ItemPut;

    public AudioClip OrderPick;

    public AudioClip OrderPut;

    public AudioClip MakeBox1;

    public AudioClip MakeBox2;

    public AudioClip SendBox;

    AudioSource _Source;

    public enum SE_Type
    {
        Countdown,

        GameStart,

        GameEnd,

        ItemPick,

        ItemPut,

        OrderPick,

        OrderPut,

        MakeBox1,

        MakeBox2,

        SendBox,
    }

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }

    void Start()
    {
        _Source = gameObject.GetComponent<AudioSource>();
    }

    public void CallSE(SE_Type type)
    {
        AudioClip clip = null;

        switch (type)
        {
            case SE_Type.Countdown:
                clip = Countdown;
                break;

            case SE_Type.GameStart:
                clip = GameStart;
                break;

            case SE_Type.GameEnd:
                clip = GameEnd;
                break;

            case SE_Type.ItemPick:
                clip = ItemPick;
                break;

            case SE_Type.ItemPut:
                clip = ItemPut;
                break;

            case SE_Type.OrderPick:
                clip = OrderPick;
                break;

            case SE_Type.OrderPut:
                clip = OrderPut;
                break;

            case SE_Type.MakeBox1:
                clip = MakeBox1;
                break;

            case SE_Type.MakeBox2:
                clip = MakeBox2;
                break;

            case SE_Type.SendBox:
                clip = SendBox;
                break;
        }

        CallSE_Core(clip);
    }

    private void CallSE_Core(AudioClip clip)
    {
        if (_Source == null || clip == null)
            return;

        _Source.clip = clip;
        _Source.Play();
    }

}
