namespace _Main._Scripts.SaveLoad
{
    using UnityEngine;

    public static class SaveManager
    {
        private static readonly ISaveManager saveManager;
        static SaveManager()
        {
            saveManager = new JsonSaveManager();
        }
        public static void Save<T>(T data) => saveManager.Save(data);
        public static T Load<T>() where T : new() => saveManager.Load<T>();

    }
    public class JsonSaveManager : ISaveManager
    {
        public T Load<T>() where T : new()
        {
            var res = JsonUtility.FromJson<T>(PlayerPrefs.GetString(typeof(T).ToString()));
            if (res == null)
                res = new T();
            return res;
        }
        public void Save<T>(T data)
        {
            PlayerPrefs.SetString(typeof(T).ToString(), JsonUtility.ToJson(data));
        }
    }
    public interface ISaveManager
    {
        void Save<T>(T data);
        T Load<T>() where T : new();
    }

}