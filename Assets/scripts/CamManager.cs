using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class CamManager : MonoBehaviour
{

   public Transform target;
   public Transform spheremask;
   public Transform RaySource;
   // The distance in the x-z plane to the target
   //public float distance = 2.5f;
   // the height we want the camera to be above the target
   //public float height = 2f;
   // How much we 
   //public float heightDamping = 3;
   //public float rotationDamping = 3;

   public Vector3 offset;
   //public float smoothTime = 0.2f;

   //public Vector3 camPos;

   //public bool isFollowX;
   //public bool isFollowY;

   //public Vector4 LimitBounday;

   Vector3 target_off;

   public float distance;
   public float smoothSpeed;

   public bool isFollow;

   public Transform[] camWork;

   public InputField inputCamDist;

   public InputField inputAngleX;
   public InputField inputAngleY;
   public InputField inputAngleZ;

   public InputField inputOffsetX;
   public InputField inputOffsetY;
   public InputField inputOffsetZ;

   Transform lookTarget;
   //public float dist;
   //public float up;
   //public float right = 0f;
   // Use this for initialization
   void Start()
   {
      //offset = transform.position - Vector3.forward;

      if (isFollow == true)
      {
         transform.position = target.position + offset;
         target_off = transform.position - target.position;
      }

      inputCamDist.text = distance.ToString();

      inputAngleX.text = transform.localEulerAngles.x.ToString();
      inputAngleY.text = transform.localEulerAngles.y.ToString();
      inputAngleZ.text = transform.localEulerAngles.z.ToString();

      inputOffsetX.text = offset.x.ToString();
      inputOffsetY.text = offset.y.ToString();
      inputOffsetZ.text = offset.z.ToString();

      //DOTween.Init();

      //Transform[] path = new Transform[camWork[0].childCount];
      //for (int i=0; i<camWork[0].childCount; i++)
      //{
      //   path[i] = camWork[0].GetChild(i);
      //}

      //lookTarget = path[path.Length - 1];
      //iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", 1f, "easetype", iTween.EaseType.linear, "orienttopath", true, "looktime", 0.01f));
      //transform.DOPath(path, 1f, PathType.Linear, PathMode.Ignore).SetLookAt(0.01f);
   }

   // Update is called once per frame
   void Update()
   {
      //Vector3 pos = target.position;
      //Vector3 desiredPosition = transform.position * (target.position - transform.position).magnitude ;// + target.right * camPos.x + target.up * camPos.y + target.forward * camPos.z;
      //if (isFollowX) desiredPosition += (target.right * camPos.x);
      //if (isFollowY) desiredPosition += (target.up * camPos.y);
      //else desiredPosition = new Vector3(desiredPosition.x, camPos.y, desiredPosition.z);
      //desiredPosition += (target.forward);
      //desiredPosition += target.right * 3f;
      //Vector3 smoothPosition = Vector3.Lerp(transform.position, target.position+target_off, smoothTime);
      //smoothPosition -= transform.right;

      if (isFollow == false)
      {
         return;
      }

      Vector3 pos = target.position + offset;
      pos -= transform.forward * distance;

      transform.position = pos;// Vector3.Lerp(transform.position, pos, smoothSpeed * Time.deltaTime);

      //RaycastHit hit;

      //if (Physics.Raycast(RaySource.position, (spheremask.transform.position - transform.position).normalized, out hit, Mathf.Infinity))
      //{
      //   if (hit.collider.gameObject.tag == "Player") return;
      //   if (hit.collider.gameObject.tag == "spheremask")
      //   {
      //      MaskScale(0, 2f);
      //   }
      //   else
      //   {
      //      MaskScale(5, 2f);
      //   }

      //   //print(hit.collider.gameObject.tag);
      //}
         
      
      
      //new Vector3(Mathf.Clamp(smoothPosition.x, LimitBounday.x, LimitBounday.y), Mathf.Clamp(smoothPosition.y, LimitBounday.w, LimitBounday.z), -10f);
      //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, target.localEulerAngles.y - 90f, transform.localEulerAngles.z);
      //LookAtSmooth.Look(transform, target.position + transform.right * distance + transform.up * height, 5f);
      //transform.LookAt(target.position + transform.right * 2.5f + transform.up * 2f);
      //if (target)
      //{
      //  // Calculate the current rotation angles
      //  float wantedRotationAngle = target.eulerAngles.y;
      //  float wantedHeight = target.position.y + height;

      //  float currentRotationAngle = transform.eulerAngles.y;
      //  float currentHeight = transform.position.y;

      //  // Damp the rotation around the y-axis
      //  currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

      //  // Damp the height
      //  currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

      //  // Convert the angle into a rotation
      //  Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

      //  // Set the position of the camera on the x-z plane to:
      //  // distance meters behind the target

      //  Vector3 pos = target.position;
      //  pos -= currentRotation * Vector3.forward * distance;
      //  pos.y = currentHeight;
      //  transform.position = pos;


      //  // Always look at the target
      //  transform.LookAt(target);
      //}
   }

   private void MaskScale(float scale, float time)
   {      
      //iTween.ScaleTo(spheremask.gameObject, Vector3.one * scale, time);
   }

   public void CamDist(string v)
   {
      float f = 0;
      float.TryParse(v, out f);
      distance = f;
   }

   public void CamAngleX(string v)
   {
      float f = 0;
      float.TryParse(v, out f);
      Vector3 a = transform.localEulerAngles;
      transform.localEulerAngles = new Vector3(f, a.y, a.z);
   }

   public void CamAngleY(string v)
   {
      float f = 0;
      float.TryParse(v, out f);
      Vector3 a = transform.localEulerAngles;
      transform.localEulerAngles = new Vector3(a.x, f, a.z);
   }

   public void CamAngleZ(string v)
   {
      float f = 0;
      float.TryParse(v, out f);
      Vector3 a = transform.localEulerAngles;
      transform.localEulerAngles = new Vector3(a.x, a.y, f);
   }

   public void CamOffsetX(string v)
   {
      float f = 0;
      float.TryParse(v, out f);

      offset.x = f;
   }

   public void CamOffsetY(string v)
   {
      float f = 0;
      float.TryParse(v, out f);

      offset.y = f;
   }

   public void CamOffsetZ(string v)
   {
      float f = 0;
      float.TryParse(v, out f);

      offset.z = f;
   }

   //   public static Follower instance;

   //   public Transform target;

   //   public float smoothTime = 0.125f;
   //   ///Vector3 velocity = Vector3.zero;
   //   public Vector3 offset;

   //   void Start () {
   //       instance = this;
   //}

   //// Update is called once per frame
   //void Update () { 
   //       //Vector3 targetPosition = target.TransformPoint(new Vector3(0f, 7.01f, -1.43f));
   //       //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

   //       Vector3 desiredPosition = target.position + offset;
   //       Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime);
   //       transform.position = smoothPosition;

   //       transform.LookAt(target);
   //   }

   //   public void SetOffset(Vector3 os)
   //   {
   //       offset = os;
   //   }

   //   public void SetFace(string name)
   //   {
   //       if (name == "UP")
   //           offset = new Vector3(0, 5, -5);
   //       else if (name == "Left")
   //           offset = new Vector3(-3f, 5f, -5f);
   //       else if (name == "Right")
   //           offset = new Vector3(3f, 5f, -5f);
   //       else if (name == "Down")
   //           offset = new Vector3(0f, -5f, -5f);
   //   }
}
