using UnityEngine;

[CreateAssetMenu(fileName = "SavesDataConfig", menuName = Constants.EditorMenuName + "/SavesData")]
public class SavesDataConfig : ScriptableObject
{
    [SerializeField] private SavesData _initialSavesData;
    [SerializeField] private string _filename = "saves";    

    public string Filename => _filename;

    public SavesData SavesData => _initialSavesData;
}