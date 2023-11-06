using TshirtInventoryBackend.Repositories.Common;

namespace TshirtInventoryBackend.Models
{
    public class Role : IEntityPKID
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
