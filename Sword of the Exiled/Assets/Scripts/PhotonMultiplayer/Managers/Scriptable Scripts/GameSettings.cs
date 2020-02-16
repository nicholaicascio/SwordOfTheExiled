using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _gameVersion = "0.0.0";
    public string GameVersion { get { return _gameVersion; } }
    [SerializeField]
    private string _nickName = "Punfish";

    public string NickName
    {
        get
        {
            return _nickName;
        }
        set
        {
            _nickName = value;
        }
    }
}