using System.Collections.Generic;
using UnityEngine;

namespace ExchangeSample.Scripts
{
    public enum CharacterType
    {
        Liquid = 0,
        Solid,
        Gas
    }

    public class Character : MonoBehaviour
    {
        [SerializeField] private List<GameObject> CharacterList;
        private CharacterType characterType;

        public AudioManager audioManager;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            characterType = CharacterType.Liquid;
        }

        // Update is called once per frame
        void Update()
        {
        }

        //  キャラクターチェンジ
        public void ChangeCharacter(string tag, Vector3 position)
        {
            //  いったん、全タイプを無効化
            foreach (var character in CharacterList)
            {
                character.SetActive(false);
            }

            //  設定されたキャラクターを有効化
            switch (tag)
            {
                case "ToLiquid":
                    characterType = CharacterType.Liquid;
                    audioManager.audioSource.PlayOneShot(audioManager.chengePlayer);
                    break;
                case "ToSolid":
                    characterType = CharacterType.Solid;
                    audioManager.audioSource.PlayOneShot(audioManager.chengePlayer);
                    break;
                case "ToGas":
                    characterType = CharacterType.Gas;
                    audioManager.audioSource.PlayOneShot(audioManager.chengePlayer); 
                    break;
            }

            CharacterList[(int)characterType].SetActive(true);
            CharacterList[(int)characterType].transform.position = position;

            //  PlayerHPに現在稼働中のPlayerObjectを設定(HORIKOSHI Masahiro)
            var playerHP = GetComponent<PlayerHP>();
            if (playerHP != null)
            {
                playerHP.CurrentCharacter = CharacterList[(int)characterType];
            }
        }
    }
}