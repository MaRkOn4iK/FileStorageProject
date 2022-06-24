using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("FullFileInfo")]
    public partial class FullFileInfo
    {
        public int Id { get; set; }

        public int? FileId { get; set; }

        public int? UserId { get; set; }

        public int? FileSecureLevelId { get; set; }

        public virtual File File { get; set; }

        public virtual FileSecureLevel FileSecureLevel { get; set; }

        public virtual User User { get; set; }
    }
}
