using UnityEngine;

[CreateAssetMenu(fileName = "NewSavesData", menuName = "ScriptableObject/NewSavesData", order = 0)]
public class SavesDataConfig : ScriptableObject
{
    [SerializeField] private SavesData _initialSavesData;
    [SerializeField] private string _filename = "saves";

    public string Filename => _filename;

    public SavesData SavesData => _initialSavesData;
}