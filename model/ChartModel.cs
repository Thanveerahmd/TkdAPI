using System.Collections;
using System.Collections.Generic;
public class ChartModel
{
    public List<string> Data { get; set; }
    public string Label { get; set; }
 
    public ChartModel()
    {
        Data = new List<string>();
    }
}