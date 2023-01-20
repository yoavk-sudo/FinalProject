namespace FinalProject.Elements
{
    internal static class ElementsList
    {
        static List<dynamic> ElementList = new List<dynamic>();
        static public void ElementByCoordinates(int[] cor)
        {
            foreach (dynamic element in ElementList)
            {
                //if(element.Coordinates() == cor) return element;
            }
        }
        static public void AddToList(dynamic element)
        {
            ElementList.Add(element);
        }
        static public void RemoveFromList(dynamic element)
        {
            ElementList.Remove(element);
        }
        static public void CreateElement(char element)
        {

        }
        static public void InteractWithElement(dynamic element)
        {

        }
    }
}