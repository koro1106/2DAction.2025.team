using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
namespace ExchangeSample.Scripts
{
    public enum CharacterType
    {
        Liquid = 0,
        Solid,
        Gas
    }

    public class Character2 : MonoBehaviour
    {
        [SerializeField] private List<GameObject> CharacterList;
        private CharacterType characterType;

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
                    break;
                case "ToSolid":
                    characterType = CharacterType.Solid;
                    break;
                case "ToGas":
                    characterType = CharacterType.Gas;
                    break;
            }

            CharacterList[(int)characterType].SetActive(true);
            CharacterList[(int)characterType].transform.position = position;
        }
    }
}*/
