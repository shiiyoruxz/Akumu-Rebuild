using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codes.Scripts.SaveSystem
{
    public class PlayerSaveData : MonoBehaviour
    {
        private PlayerData _playerData = new PlayerData();

        public void SetCurrentLevel(Vector3 currentCheckPoint)
        {
            _playerData.playerPosition = currentCheckPoint;
        }
       
        public PlayerData GetPlayerData()
        {
            return _playerData;
        }
       
        public void SavePlayerData()
        {
            SaveGameManager.CurrentSaveData.PlayerData = _playerData;
            SaveGameManager.SaveData();
        }
       
        public void LoadPlayerData()
        {
            SaveGameManager.LoadData("SaveGame.sav");
            _playerData = SaveGameManager.CurrentSaveData.PlayerData;
        }

        void Update()
        {
            if (GameOver.playerIsDead && GameOver.playerWantRetry)
            {
                LoadPlayerData();
                SceneManager.LoadScene(2);
                GameOver.playerWantRetry = false;
                GameOver.playerIsDead = false;
            }
            StartCoroutine(trackCheckPoint());
        }

        IEnumerator trackCheckPoint()
        {
            yield return new WaitForSeconds(30.0f);
            _playerData.playerPosition = transform.position;
            _playerData.playerRotation = transform.rotation;
            SavePlayerData();
        }

    }
    

    
    [System.Serializable]
    public struct PlayerData
    {
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public string checkpoint;
    }
}