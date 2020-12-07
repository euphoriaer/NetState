using UnityEngine;



    public class SingletnoAutoMono<T> : MonoBehaviour where T : SingletnoAutoMono<T>
    {
        public static T instance;

        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            instance = this as T;
        }
    }
