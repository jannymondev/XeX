using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    #region variables

    #region camera
    [Header("Camera variables")]

    [Tooltip("camera transform")]
    public Transform cam;

    [Tooltip("camera forward")]
    public Transform cameraForward;

    #endregion

    [Space(10)]

    #region localTransforms
    [Header("local Transforms")]

    [Tooltip("this looks at the camera")]
    public Transform cameraLooker;

    [Tooltip("the base forward")]
    public Transform baseForward;

    [Tooltip("goes to the forward 'point' ")]
    public Transform forwardPoint;

    [Tooltip("looks at forwardPoint")]
    public Transform localForward;

    #endregion

    [Space(10)]

    #region movementFloats

    [Header("movement floats")]
    [Tooltip("movement speed of forwardPoint")]
    //[Range(0f, 1f)]
    public float forwardPointSpeed;

    [Tooltip("this is the animationcurve (speed) of forwardPointSpeed")]
    public AnimationCurve fpms;

    #endregion

    [Space(10)]

    #region otherFloats
    [Header("other floats")]

    [Tooltip("divides speed by topGenericSpeed, then clamps between 0 and 1. To evaluate animation curve")]
    public float genericSpeed;

    [Tooltip("topGenericSpeed (used for above)")]
    public float topGenericSpeed;

    #endregion

    [Space(10)]

    #region physics
    [Header("Physics")]

    [Tooltip("the rigidbody")]
    public Rigidbody rb;

    [Tooltip("rigidbody velocity")]
    public float velocity;

    #endregion

    [Space(10)]

    #region input
    [Header("Input")]
    public Vector2 axis;
    #endregion

    #endregion

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        velocity = rb.velocity.magnitude;
        genericSpeed = Mathf.Clamp(velocity, 0f, topGenericSpeed) / topGenericSpeed;
        forwardPointSpeed = fpms.Evaluate(genericSpeed);
        axis = Vector2.Lerp(axis, new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), forwardPointSpeed);
        cameraLooker.LookAt(cameraForward);
        baseForward.localEulerAngles = new Vector3(0f, cameraLooker.eulerAngles.y, 0f);
        forwardPoint.localPosition = new Vector3(axis.x, 0f, axis.y);
	}
}