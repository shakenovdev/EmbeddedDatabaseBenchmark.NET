namespace Foundation.Core
{
    public class CoreDataUnit : IDataUnit
    {
        public string Name { get; set; }
        public int Sum { get; set; }

        public void UpdateName()
        {
            Name = Helper.RandomString(10);
        }
    }
}