using Foundation;
using Foundation.Core;
using Realms;

namespace RealmUnit
{
    public class RealmDataUnit : RealmObject, IDataUnit
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sum { get; set; }
        
        // get unmanaged copy of object
        internal RealmDataUnit ToStandaloneWithUpdatedName()
        {
            return new RealmDataUnit
            {
                Id = Id,
                Name = Helper.RandomString(10),
                Sum = Sum
            };
        }
    }
}