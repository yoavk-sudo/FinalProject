using System.Reflection;

namespace FinalProject
{
    internal struct Save
    {
        public static void SaveGame(Player player)
        {
            Type type = player.GetType();
            
            PropertyInfo[] properties = type.GetProperties();
            string[] datas = new string[properties.Length];
            int i = 0;
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Coordinates" || property.Name == "Direction")
                {
                    i++;
                    continue;
                }
                datas[i++] = property.Name + " " + property.GetValue(player);
            }
            File.WriteAllLinesAsync("Saves.txt", datas);
            //File.AppendAllLines("Saves.txt",datas);
        }
    }
}
