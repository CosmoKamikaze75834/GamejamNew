using System;

public class DifficultySwitcher
{
    public static DifficultyType Difficulty { get; private set; }

    public static event Action Changed;

    public void SetDifficulty(DifficultyType difficulty)
    {
        if (Equals(Difficulty, difficulty) == false)
        {
            Difficulty = difficulty;
            Changed?.Invoke();
        }
    }

    public void Next()
    {
        Array values = Enum.GetValues(typeof(DifficultyType));
        int currentIndex = Array.IndexOf(values, Difficulty);
        int nextIndex = (currentIndex + 1) % values.Length;
        SetDifficulty((DifficultyType)values.GetValue(nextIndex));
    }

    public void Previous()
    {
        Array values = Enum.GetValues(typeof(DifficultyType));
        int currentIndex = Array.IndexOf(values, Difficulty);
        int previousIndex = (currentIndex - 1 + values.Length) % values.Length;
        SetDifficulty((DifficultyType)values.GetValue(previousIndex));
    }
}