using System.Text.Json;


namespace GetMediaCore.Traffic {
    internal class Accessor {
        // Your data structure
        private int[][] data;
        private string filePath = "mydata.json";

        public Accessor() {
            data = new int[3][];
            data[0] = new int[150]; // Fill with your values
            data[1] = new int[150];
            data[2] = new int[150];
        }

        // SAVE: One line to turn the whole thing into a file
        public void Save() {
            File.WriteAllText(filePath, JsonSerializer.Serialize(data));
        }

        // READ: One line to pull it all back into an array
        public int[][] Load() {
            if (!File.Exists(filePath))
                return data;

            var loadedData = JsonSerializer.Deserialize<int[][]>(File.ReadAllText(filePath));
            if (loadedData != null)
                data = loadedData;
            return data;
        }

        public int[][] Data => data;
    }
}
