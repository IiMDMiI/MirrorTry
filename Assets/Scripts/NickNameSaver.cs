using UnityEngine;
using TMPro;
using Gameplay;

public class NickNameSaver : MonoBehaviour
{   
    [SerializeField] private TMP_InputField _inputField;
    private void Awake() => 
        _inputField.onEndEdit.AddListener(SetPlayerName);
    private void SetPlayerName(string name) =>
        PlayerPrefs.SetString(Player.NameKey, _inputField.text);

}
