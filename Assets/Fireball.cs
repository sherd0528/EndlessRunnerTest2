using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
   public AudioClip ������;
   public AudioClip ������;

   // Start is called before the first frame update
   void Start()
   {
      AudioSource.PlayClipAtPoint(������, transform.position);
   }

   // Update is called once per frame
   void Update()
   {

   }

   private void OnParticleSystemStopped()
   {
      
   }

   private void OnParticleCollision(GameObject other)
   {
      AudioSource.PlayClipAtPoint(������, transform.position);
      Destroy(gameObject, 5);
   }
}
