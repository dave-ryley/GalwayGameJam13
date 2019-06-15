using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CameraGroupManager : MonoBehaviour
{
    private CinemachineTargetGroup _targetGroup;
    // Start is called before the first frame update
    void Start()
    {
        _targetGroup = GetComponent<CinemachineTargetGroup>();
        GGJGameManager.RegisterCameraGroup(this);
    }

    public void AddTarget(Transform target)
    {
        _targetGroup.AddMember(target, 1f, 1f);
    }
}
