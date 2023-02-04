namespace FinalProject.Elements
{
    internal static class EnemyList
    {
        static List<Enemy> Enemies = new List<Enemy>();
        static public Enemy? EnemyByCoordinates(int[] cor)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.Coordinates == cor) return enemy;
            }
            return null;
        }
        static public void AddToList(Enemy enemy)
        {
            Enemies.Add(enemy);
        }
        static public void RemoveFromList(Enemy enemy)
        {
            Enemies.Remove(enemy);
        }
    }
}
