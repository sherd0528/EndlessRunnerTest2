using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shooter : MonoBehaviour
{
   public ParticleSystem bullet;
   public Transform target;

   // Start is called before the first frame update
   void Start()
   {
      Shot(5f);
   }

   // Update is called once per frame
   void Update()
   {

   }

   public void Shot(float height)
   {
      var path = new Vector3[6];

      var vec_center = Vector3.Lerp(transform.position, target.position, 0.5f);
      path[0] = new Vector3(vec_center.x, vec_center.y + height, vec_center.z);      
      path[1] = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
      vec_center = Vector3.Lerp(transform.position, target.position, 0.25f);
      path[2] = new Vector3(vec_center.x, vec_center.y + height, vec_center.z);
      path[3] = target.position;
      vec_center = Vector3.Lerp(transform.position, target.position, 0.75f);
      path[4] = new Vector3(vec_center.x, vec_center.y + height, vec_center.z);
      path[5] = target.position + target.up;

      //path[0] = transform.position;
      //var vec_center = Vector3.Lerp(transform.position, target.position, 0.25f);
      //path[1] = new Vector3(vec_center.x, vec_center.y + height / 2f, vec_center.z);
      //vec_center = Vector3.Lerp(transform.position, target.position, 0.5f);
      //path[2] = new Vector3(vec_center.x, vec_center.y + height, vec_center.z);
      //vec_center = Vector3.Lerp(transform.position, target.position, 0.9f);
      //path[3] = new Vector3(vec_center.x, vec_center.y + height/2f, vec_center.z);
      //path[4] = target.position;

      bullet.transform.DOPath(path, 3f, PathType.CubicBezier);
   }
}
