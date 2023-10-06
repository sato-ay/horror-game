using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

public class PlayerMove : MonoBehaviour
{
    float x, z;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject sensityvitySetting;
    [SerializeField] private CurveControlledBob headBob_ = new CurveControlledBob();

    private Camera cam_;
    private Transform cameraTransform;
    private GameObject cam;
    Quaternion cameraRot, characterRot;

    private AudioSource audioSource;
    private AudioClip audioClip;
    
    private bool cursorLock = true;
    private bool cursorVisible = true;
    private bool step, stepSEPlaying;

    //角度の制限
    float minX = -90f, maxX = 90f;

    Slider sensityvitySlider;
    float sensityvity;

    void Start()
    {
        cam = Camera.main.gameObject;
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;

        cam_ = cam.GetComponent<Camera>();
        cameraTransform = cam_.GetComponent<Transform>();

        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;

        sensityvitySlider = sensityvitySetting.GetComponent<Slider>();
        sensityvity = 0.7f;

        headBob_.Setup(cam_, 1.0f);
    }

    void LateUpdate()
    {
        if (sensityvitySetting.activeSelf)
        {
            double _sensityvity = sensityvitySlider.value;
            sensityvity = float.Parse(_sensityvity.ToString("f2"));
        }

        float xRot = cursorLock ? Input.GetAxis("Mouse X") * sensityvity : 0.0f;
        float yRot = cursorLock ? Input.GetAxis("Mouse Y") * sensityvity : 0.0f;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        UpdateCursorLock();
    }

    private void FixedUpdate()
    {
        x = 0;
        z = 0;

        step = (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) ? true : false;
        if (cursorLock && cursorVisible)
        {
            if (step)
            {
                Vector3 handBob = headBob_.DoHeadBob(1.5f);
                cameraTransform.localPosition = handBob;
            }
            else
            {
                Vector3 handBob = headBob_.DoHeadBob(0.1f);
                cameraTransform.localPosition = handBob;
            }
        }

        StepSE();

        x = cursorLock ? Input.GetAxisRaw("Horizontal") * speed : 0;
        z = cursorLock ? Input.GetAxisRaw("Vertical") * speed : 0;

        transform.position += cam.transform.forward * z + cam.transform.right * x;
    }

    public void UpdateCursorLock()
    {
        if (cursorVisible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(!cursorVisible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX,minX,maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    public bool CursorLock
    {
        get { return cursorLock; }
        set { cursorLock = value; }
    }

    public bool CursorVisible
    {
        get { return cursorVisible; }
        set { cursorVisible = value; }
    }

    private void StepSE()
    {
        if (step && !stepSEPlaying && cursorLock)
        {
            stepSEPlaying = true;
            audioSource.PlayOneShot(audioClip);
        }
        if (!step && stepSEPlaying)
        {
            stepSEPlaying = false;
            audioSource.Stop();
        }
    }

    public void CursorAllFree()
    {
        CursorLock = false;
        CursorVisible = false;
    }
}
