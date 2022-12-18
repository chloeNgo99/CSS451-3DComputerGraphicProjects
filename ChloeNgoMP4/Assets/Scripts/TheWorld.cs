using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class TheWorld : MonoBehaviour  {

    public SceneNode BaseNode;
    public Transform BaseJoin;
    public SceneNode Bneck;
    public Transform NeckJoin;

    public SceneNode RightHand;
    public Transform RightHandJoin;
    public SceneNode LeftHand;
    public Transform LeftHandJoin;

    public SceneNode LeftArm;
    public Transform LeftArmJoin;
    public SceneNode RightArm;
    public Transform RightArmJoin;

    public SceneNode LeftPalm;
    public Transform LeftPalmJoin;
    public SceneNode RightPalm;
    public Transform RightPalmJoin;


    public SceneNode thirdHand;
    public Transform thirdHandJoin;
    public SceneNode thirdArm;
    public Transform thirdArmJoin;
    public SceneNode thirdPalm;
    public Transform thirdPalmJoin;

    public Transform eye, head, torso, body;

    public SceneNode TorsoBase;
    

    //public Transform TargetPos;

    public bool TrackTarget = false;
    public bool RotateRoot = false;
    public float RootDelta = 0f;

    public bool RotateChild = false;
    public float ChildDelta = 0.5f;

    private float FrontHeight = 8.0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        //TheRoot.OrbitAroundWorldY(0.4f); // degree
        UpdateHierarchy();
        /*
        if (TrackTarget) {            
            // do root
            if (RotateRoot) {    
                Vector3 rootDir = TargetPos.localPosition - BaseJoin.localPosition;
                TheRoot.RotateUpTowardsBy(rootDir, RootDelta);
                UpdateHierarchy();
            }

            if (RotateChild) {
                Vector3 childDir = TargetPos.localPosition - NeckJoin.localPosition;
                Bneck.RotateUpTowardsBy(childDir, ChildDelta);
                UpdateHierarchy();
            }

            Vector3 dir = TargetPos.localPosition - RightHandJoin.localPosition;
            RightHand.AlignUpWith(dir);
            UpdateHierarchy();
        }
        */
    }

    private void UpdateHierarchy() {
        
        Matrix4x4 i = Matrix4x4.identity;
        BaseNode.CompositeXform(ref i);

        BaseNode.SetAxisFrame(BaseJoin);
        TorsoBase.SetAxisFrame(BaseJoin);
        Bneck.SetAxisFrame(NeckJoin);
        RightHand.SetAxisFrame(RightHandJoin);
        LeftHand.SetAxisFrame(LeftHandJoin);
        LeftArm.SetAxisFrame(LeftArmJoin);
        RightArm.SetAxisFrame(RightArmJoin);
        LeftPalm.SetAxisFrame(LeftPalmJoin);
        RightPalm.SetAxisFrame(RightPalmJoin);

        thirdHand.SetAxisFrame(thirdHandJoin);
        thirdArm.SetAxisFrame(thirdArmJoin);
        thirdPalm.SetAxisFrame(thirdPalmJoin);
        //eye.localPosition = new Vector3(head.localPosition.x, head.localPosition.y + 4, head.localPosition.z-2);
        //Bneck.SetAxisFrame(eye);
        //FrontTip.localPosition = RightHandJoin.localPosition + FrontHeight * RightHandJoin.up;
        //FrontTip.localRotation = RightHandJoin.localRotation;
    }
}
