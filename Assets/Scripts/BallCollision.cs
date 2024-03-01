using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCollision : MonoBehaviour
{
    public GameObject cylinder1;
    public Slider speedSlider;
    public GameObject basketball;
    public Transform cameraTransform;
    public GameInfo gameinfo;
    private AudioManager audioManager;
    private int ballCount;
    private bool hasCollidedWithGround = false;
    private float chargeTime;
    private float currentSpeed;
    private float startTime;
    private bool isCharging;
    private float currentChargeValue = 0f;
    private bool increasing = true;
    public float minSpeed = 5f;
    public float maxSpeed = 15f;
    public float maxChargeTime = 2.0f;

    private bool canCharge = true; // 是否可以进行蓄力
    private void Start()
    {
        ballCount = gameinfo.ballCount;
        audioManager = AudioManager.instance;
    }

    private void Update()
    {
        CheckBallCollisions();
    }
    private void CheckBallCollisions()
    {
        if (gameObject.CompareTag("Basketball") && hasCollidedWithGround)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb.velocity.magnitude < 1.5f)
            {
                Disappear();
            }
        }
    }


    private void Disappear()
    {
        GameInfo.instance.ballCount--;
        GameInfo.instance.UpdateBallSumText();
        RemoveFromClothCollider(gameObject);
        Destroy(gameObject);
    }

    private void RemoveFromClothCollider(GameObject obj)
    {
        Cloth cloth = cylinder1.GetComponent<Cloth>();
        SphereCollider sphereCollider = obj.GetComponent<SphereCollider>();
        List<SphereCollider> sphereColliders = new List<SphereCollider>();
        foreach (var colliderPair in cloth.sphereColliders)
        {
            sphereColliders.Add(colliderPair.first);
        }

        if (sphereColliders.Contains(sphereCollider))
        {
            sphereColliders.Remove(sphereCollider);
        }

        ClothSphereColliderPair[] colliderPairs = new ClothSphereColliderPair[sphereColliders.Count];
        for (int i = 0; i < sphereColliders.Count; i++)
        {
            colliderPairs[i] = new ClothSphereColliderPair(sphereColliders[i]);
        }
        cloth.sphereColliders = colliderPairs;
    }

    private void AddToClothCollider(GameObject obj)
    {
        Cloth cloth = cylinder1.GetComponent<Cloth>();
        SphereCollider sphereCollider = obj.GetComponent<SphereCollider>();
        List<SphereCollider> sphereColliders = new List<SphereCollider>();
        foreach (var colliderPair in cloth.sphereColliders)
        {
            sphereColliders.Add(colliderPair.first);
        }

        sphereColliders.Add(sphereCollider);

        ClothSphereColliderPair[] colliderPairs = new ClothSphereColliderPair[sphereColliders.Count];
        for (int i = 0; i < sphereColliders.Count; i++)
        {
            colliderPairs[i] = new ClothSphereColliderPair(sphereColliders[i]);
        }
        cloth.sphereColliders = colliderPairs;
    }
}
