namespace GitHub_KPI_tool_Application.Calculator;

public static class Calculator
{
    public static void Add(Dictionary<string, double> result, Dictionary<string, double> part, double value)
    {
        foreach (var pair in part)
        {
            if (!result.TryAdd(pair.Key, Math.Pow(pair.Value * 100.0 / value, 2)))
            {
                result[pair.Key] += Math.Pow(pair.Value * 100.0 / value, 2);
            }
        }
    }

    public static Dictionary<string, int> CalculateMarks(IDictionary<string, double> result)
    {
        var sortedPairs = result.ToList();
        sortedPairs.Sort((x, y) => x.Value.CompareTo(y.Value));
        var differences = new List<KeyValuePair<int,double>>();
        for (int i = 1; i < sortedPairs.Count; i++)
        {
            differences.Add(KeyValuePair.Create(i, sortedPairs[i].Value-sortedPairs[i-1].Value));
        }
        differences.Sort((x, y) => x.Value.CompareTo(y.Value));
        differences.Reverse();
        differences = differences.GetRange(0, 4);

        var marks = new Dictionary<string, int>();
        int mark = 1;
        int counter = 1;
        foreach (var pair in sortedPairs)
        {
            marks.Add(pair.Key, mark);
            if (differences.Exists((x) => x.Key == counter))
            {
                mark++;
            }
            counter++;
        }
        
        return marks;
    }
}