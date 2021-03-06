﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    #region Private Fields

    [Tooltip("The distance in the local x-z plane to the target")]
    [SerializeField]
    float distance = 7.0f;

    [Tooltip("The height we want the camera to be above the target")]
    [SerializeField]
    float height = 3.0f;


    [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view of the sceneray and less ground.")]
    [SerializeField]
    Vector3 centerOffset = Vector3.zero;


    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
    [SerializeField]
    bool followOnStart = false;


    [Tooltip("The Smoothing for the camera to follow the target")]
    [SerializeField]
    float smoothSpeed = 0.125f;

    Transform cameraTransform;

    bool isFollowing;

    Vector3 cameraOffset = Vector3.zero;

    #endregion

    #region MonoBehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        if (followOnStart)
        {
            OnStartFollowing();
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null && isFollowing)
        {
            OnStartFollowing();
        }

        if (isFollowing)
        {
            Follow();
        }
    }

    #endregion

    #region Public Methods

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;

        Cut();
    }

    #endregion

    #region Private Fields

    void Follow()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;


        cameraTransform.position = Vector3.Lerp(cameraTransform.position, transform.position + transform.TransformVector(cameraOffset), smoothSpeed * Time.deltaTime);


        cameraTransform.LookAt(transform.position + centerOffset);
    }

    void Cut()
    {
        cameraOffset.z = -distance;
        cameraOffset.y = height;


        cameraTransform.position = transform.position + transform.TransformVector(cameraOffset);


        cameraTransform.LookAt(transform.position + centerOffset);
    }

    #endregion
}
