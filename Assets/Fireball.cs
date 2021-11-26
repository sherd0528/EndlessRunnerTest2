using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
   public AudioClip 시작음;
   public AudioClip 폭발음;

   // Start is called before the first frame update
   void Start()
   {
      AudioSource.PlayClipAtPoint(시작음, transform.position);
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
      AudioSource.PlayClipAtPoint(폭발음, transform.position);
      Destroy(gameObject, 5);
   }
}
