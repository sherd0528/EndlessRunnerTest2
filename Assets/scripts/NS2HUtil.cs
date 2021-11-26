//using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using GoogleMobileAds.Api;
using UnityEngine.UI;

using System.Linq;

namespace NS2HUtil
{
   public class ShuffleRandom<T>
   {
      private ArrayList arr;
      private int shuffle_range;
      private int[] randIndex;

      public ShuffleRandom()
      {
         arr = new ArrayList();
      }

      public void Init()
      {
         arr.Clear();
      }

      public void Add(T y)
      {
         arr.Add(y);
      }

      public void InitProps()
      {
         randIndex = new int[arr.Count];

         shuffle_range = arr.Count;

         for (int i = 0; i < randIndex.Length; i++)
         {
            randIndex[i] = i;
         }
      }

      public T GetNextIndex()
      {
         if (shuffle_range == 0)
            InitProps();

         int rand_pos = Random.Range(0, (arr.Count * 100) - 1) % shuffle_range;

         int currIndex = randIndex[rand_pos];
         randIndex[rand_pos] = randIndex[shuffle_range - 1];
         randIndex[shuffle_range - 1] = currIndex;

         shuffle_range--;

         return (T)arr[currIndex];
      }

      public int Count()
      {
         return arr.Count;
      }
   }

   public class NS2HUtil
   {
      public NS2HUtil()
      {
      }
   }

   public class ShowUI3DPos
   {
      //        static RectTransform canvasRectT;

      public ShowUI3DPos()
      {
         //            canvasRectT = GameObject.Find ("Canvas").GetComponent<RectTransform> ();    
      }

      static public void Show(RectTransform canvasT, Transform target, Vector3 pos)
      {
         if (canvasT == null) return;

         //print(pos.ToString());
         Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(pos);
         Vector2 WorldObject_ScreenPosition = new Vector2(
             ((ViewportPosition.x * canvasT.sizeDelta.x) - (canvasT.sizeDelta.x * 0.5f)),
             ((ViewportPosition.y * canvasT.sizeDelta.y) - (canvasT.sizeDelta.y * 0.5f)));

         target.localPosition = new Vector3(WorldObject_ScreenPosition.x, WorldObject_ScreenPosition.y, 0f);
      }
   }

   public class LookAtSmooth
   {
      public LookAtSmooth() { }
      public static void Look(Transform my, Vector3 targetPos, float damping)
      {
         Quaternion rotation = Quaternion.LookRotation(targetPos - my.position);
         //print(rotation.ToString());
         my.rotation = Quaternion.Slerp(my.rotation, rotation, Time.deltaTime * damping);
      }
   }

   public class ResourceManager
   {
      class IndexManager
      {
         public Transform tObj;
         public int index;

         public IndexManager(Transform t)
         {
            tObj = t;
            index = 0;
         }

         public Transform GetNextObject()
         {
            Transform ret = null;
            if (tObj.childCount > 0)
            {
               if (index >= tObj.childCount)
                  index = 0;
               ret = tObj.GetChild(0);
            }
            else
               ret = tObj;

            if (ret != tObj)
               ret.SetParent(null);

            return ret;
         }

         public int GetCount()
         {
            return tObj.childCount;
         }

         public void Reset()
         {
            for (int i = 0; i < tObj.childCount; i++)
            {
               tObj.GetChild(i).localRotation = Quaternion.identity;
               tObj.GetChild(i).gameObject.SetActive(false);
            }
         }

         public void SetParent(Transform t)
         {
            t.SetParent(tObj);
         }
      }

      Dictionary<string, IndexManager> Pool;

      public ResourceManager()
      {
         Pool = new Dictionary<string, IndexManager>();
      }

      public void SetPool(Transform poolRoot)
      {
         for (int i = 0; i < poolRoot.childCount; i++)
         {
            Pool.Add(poolRoot.GetChild(i).name, new IndexManager(poolRoot.GetChild(i)));
         }
      }

      public Transform GetNextObject(string name)
      {
         if (Pool.ContainsKey(name))
         {
            IndexManager im = (IndexManager)Pool[name];

            return im.GetNextObject();
         }

         return null;
      }

      public int GetCount(string name)
      {
         if (Pool.ContainsKey(name))
         {
            IndexManager im = (IndexManager)Pool[name];

            return im.GetCount();
         }

         return -1;
      }

      public void ReuseObject(Transform t)
      {
         t.gameObject.SetActive(false);
      }

      public void ReturnToOwner(string owername, Transform t)
      {
         ((IndexManager)Pool[owername]).SetParent(t);
      }


      public void Reset()
      {
         List<string> keys = new List<string>(Pool.Keys);

         for (int i = 0; i < keys.Count; i++)
         {
            Pool[keys[i]].Reset();
         }
      }
   }

   public class ResourceManagerEx
   {
      public class Indexer
      {
         int index;
         //public List<Transform> child;
         public Transform parent;

         public Indexer(Transform p)
         {
            //child = new List<Transform>();
            parent = p;
         }

         public Transform GetNextObj()
         {
            if (parent.childCount == 0) return null;

            Transform res = parent.GetChild(index++);
            res.gameObject.SetActive(true);
            index %= parent.childCount;
            return res;
         }

         public void Pause()
         {
            foreach (ParticleSystem ps in parent.GetComponentsInChildren<ParticleSystem>())
            {
               ParticleSystem.MainModule mm = ps.main;
               mm.simulationSpeed = 0f;
            }
         }
         //public void Reset()
         //{
         //   foreach (Transform t in child)
         //   {
         //      t.SetParent(parent);
         //      t.gameObject.SetActive(false);
         //   }
         //}
      }

      public Dictionary<string, Indexer> dicRes;
      
      public ResourceManagerEx()
      {
         dicRes = new Dictionary<string, Indexer>();
      }

      public ResourceManagerEx(Transform pool)
      {
         dicRes = new Dictionary<string, Indexer>();

         for (int i=0; i<pool.childCount; i++)
         {
            Transform p = pool.GetChild(i);
            dicRes.Add(p.name, new Indexer(p));
         }
      }

      public Transform GetNextObj(string key)
      {
         return dicRes[key].GetNextObj();
      }

      public void ReuseObject(Transform t)
      {
         t.gameObject.SetActive(false);
      }

      public void Pause()
      {
         foreach (string key in dicRes.Keys)
         {
            dicRes[key].Pause();
         }
      }
      //public void Reset(string key)
      //{
      //   for (int i=0; i<dicRes[key].child.Count; i++)
      //   {
      //      dicRes[key].Reset();
      //   }
      //}
   }

   /*
  public class GoogleAds
  {
     string _adsBannerId;
     string _adsInterstitialId;

     InterstitialAd interstitial;
     BannerView bannerView;

     public GoogleAds(string adsBannerId, string adsInterstitialId) 
     {
        _adsBannerId = adsBannerId;
        _adsInterstitialId = adsInterstitialId;
     }

     public void RequestBanner()
     {
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(_adsBannerId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
//			AdRequest request = new AdRequest.Builder().AddTestDevice("BE14DC1D2DF11DFD87AB45D8ED85367D").Build();
        AdRequest request = new AdRequest.Builder().Build();
//			request.TestDevices.Add("BE14DC1D2DF11DFD87AB45D8ED85367D");
        // Load the banner with the request.
        bannerView.LoadAd(request);
        bannerView.Show();
     }

     public void RequestInterstital()
     {
        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(_adsInterstitialId);
        // Create an empty ad request.
//			AdRequest request = new AdRequest.Builder().AddTestDevice("BE14DC1D2DF11DFD87AB45D8ED85367D").Build();
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
     }

     public void ShowInterstital()
     {
        if (interstitial != null && interstitial.IsLoaded())
           interstitial.Show();
     }

     public void ShowBanner()
     {
        bannerView.Show();
     }

     public void HideBanner()
     {
        bannerView.Hide();
     }
  }
*/

   public class ItemCoolTimer : MonoBehaviour
   {
      class CoolTimerProps
      {
         public Image _img;
         public float _time;

         public CoolTimerProps(Image img, float time)
         {
            _img = img;
            _time = time;
         }
      }

      public ItemCoolTimer() { }

      public void CoolDown(Image img, float time)
      {
         StartCoroutine("SC_CoolDown", new CoolTimerProps(img, time));
      }

      IEnumerator SC_CoolDown(CoolTimerProps ctp)
      {
         ctp._img.fillAmount = 1f;
         float delta = 1f / ctp._time;
         while (ctp._img.fillAmount > 0f)
         {
            ctp._img.fillAmount -= (Time.deltaTime * delta);
            yield return null;
         }

         ctp._img.fillAmount = 0f;
      }

      public void StopCoolDown()
      {
         StopCoroutine("SC_CoolDown");
      }
   }



   public class LargeNum
   {
      public int[] _num;

      public LargeNum(string num)
      {
         char[] numChar = num.ToCharArray();

         _num = new int[13];

         for (int i = 0; i < 13; i++)
         {
            _num[i] = 0;
         }

         for (int i = numChar.Length - 1; i >= 0; i--)
         {
            _num[i] = (int)(numChar[i] - (char)48);
         }
      }

      public string NumString()
      {
         string str = "";

         bool bFoundNonZero = false;

         int i = 0;
         for (i = 0; i < _num.Length; i++)
         {
            if (bFoundNonZero == false && _num[i] != 0)
            {
               bFoundNonZero = true;
               str += _num[i].ToString();
            }
            else if (bFoundNonZero == true)
               str += _num[i].ToString();
         }

         string strRes = "";
         char[] charStr = str.ToCharArray();
         //char[] strRes = new char[charStr.Length + (int)(charStr.Length / 3f)];
         int idx = 0;
         for (i = charStr.Length - 1; i >= 0; i--)
         {
            idx++;
            strRes = charStr[i].ToString() + strRes;
            if (idx % 3 == 0 && i != 0)
            {
               strRes = "," + strRes;
            }
         }

         return strRes;
      }

      public static string ToCommaString(ulong val)
      {
         string str = val.ToString();
         string strRes = "";

         char[] charStr = str.ToCharArray();

         int idx = 0;
         for (int i = charStr.Length - 1; i >= 0; i--)
         {
            idx++;
            strRes = charStr[i].ToString() + strRes;
            if (idx % 3 == 0 && i != 0)
            {
               strRes = "," + strRes;
            }
         }

         return strRes;
      }

      public void Plus(int amount)
      {
         string strNum = amount.ToString();

         int[] num = new int[strNum.Length];

         int idx = 0;
         foreach (char snum in strNum.ToCharArray())
         {
            num[idx] = int.Parse(snum.ToString());
            idx++;
         }

         int numIdx = _num.Length - 1;

         for (int i = num.Length - 1; i >= 0; i--)
         {
            int tmp = (_num[numIdx] + num[i]);
            if (tmp >= 10)
            {
               int tmpIdx = numIdx;

               int remain = tmp - 10;

               _num[tmpIdx] = remain;
               tmpIdx--;

               _num[tmpIdx] += 1;

               if (i == 0)
               {
                  tmp = _num[tmpIdx];

                  while (tmp >= 10)
                  {
                     _num[tmpIdx] = tmp - 10;
                     tmpIdx--;
                     tmp = _num[tmpIdx] + 1;
                  }

                  _num[tmpIdx] = tmp;
               }

            }
            else
               _num[numIdx] = tmp;

            numIdx--;
         }
      }

      public void Minus(int amount)
      {
         string strNum = amount.ToString();

         int[] num = new int[strNum.Length];

         int idx = 0;
         foreach (char snum in strNum.ToCharArray())
         {
            num[idx] = int.Parse(snum.ToString());
            idx++;
         }

         int numIdx = _num.Length - 1;

         for (int i = num.Length - 1; i >= 0; i--)
         {
            if (_num[numIdx] < num[i])
            {
               int tmpIdx = numIdx;
               tmpIdx--;

               while (_num[tmpIdx] == 0)
               {
                  _num[tmpIdx] = 9;
                  tmpIdx--;
               }

               _num[tmpIdx] -= 1;
               _num[numIdx] = ((10 + _num[numIdx]) - num[i]);
            }
            else
               _num[numIdx] -= num[i];

            numIdx--;
         }
      }

      public int Compare(int val)
      {
         string strNum = val.ToString();

         int[] num = new int[strNum.Length];

         int idx = 0;
         foreach (char snum in strNum.ToCharArray())
         {
            num[idx] = int.Parse(snum.ToString());
            idx++;
         }

         if (num.Length < _num.Length)
         {
            int maxIndex = _num.Length - num.Length;
            for (int i = 0; i < maxIndex; i++)
               if (_num[i] > 0)
                  return 1;
         }

         if (num.Length > _num.Length)
         {
            return -1;
         }

         idx = 0;
         for (int i = 0; i < num.Length; i++)
         {
            idx = _num.Length - (num.Length - i);
            if (_num[idx] > num[i])
               return 1;
            else if (_num[idx] < num[i])
               return -1;
         }

         return 0;
      }
   }

   public class ItemRandomPicker<T>
   {
      Dictionary<T, int> _Items;
      int _range;

      public ItemRandomPicker()
      {
         _range = 0;
         _Items = new Dictionary<T, int>();
      }

      public void AddItem(T name, int weight)
      {
         _range += weight;
         _Items.Add(name, weight);
      }

      public T PickItem()
      {
         if (_Items.Count == 0) return default(T);

         int rand = Random.Range(0, _range);
         int top = 0;

         for (int i = 0; i < _Items.Count; i++)
         {
            top += _Items.ElementAt(i).Value;
            if (top > rand)
               return _Items.ElementAt(i).Key;
         }

         return _Items.ElementAt(_Items.Count - 1).Key;
      }
   }

   public class Util
   {
      public static Transform CreatePrefab(string path)
      {
         return Object.Instantiate(Resources.Load(path) as GameObject).transform;
      }

      public static int nextLevel(int level)
      {
         return (int)Mathf.Round(0.04f * (Mathf.Pow(level, 3)) + 0.8f * (Mathf.Pow(level, 2)) + 2 * level);
      }

      public static int GetItemCost(int level)
      {
         return (int)Mathf.Round(0.04f * (Mathf.Pow(level, 4)) + 0.8f * (Mathf.Pow(level, 2)) + 2 * level) * 5;
      }

      public static void SmoothLightOnOff(float time, float min, float max, Light light)
      {
         
      }
   }
}

