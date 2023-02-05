namespace FinalProject.Elements
{
    internal static class EnemyList
    {
        static List<Enemy> Enemies = new List<Enemy>();
        static public Enemy? EnemyByCoordinates(int[] cor)
        {
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.Coordinates.SequenceEqual(cor)) return enemy;
            }
            return null;
        }
        static public void AddToList(Enemy enemy)
        {
            Enemies.Add(enemy);
        }
        static public void RemoveFromList(Enemy enemy)
        {
            LockMethods.SetCursorLockAndOneSpace(enemy.Coordinates);
            Map.ZeroCoordinate(Map.MapCol, enemy.Coordinates);
            enemy.HP = 0;
            Enemies.Remove(enemy);
        }
        public static void TerminateAllEnemies()
        {
            foreach (var enemy in Enemies)
            {
                RemoveFromList(enemy);
            }
        }
    }
}
