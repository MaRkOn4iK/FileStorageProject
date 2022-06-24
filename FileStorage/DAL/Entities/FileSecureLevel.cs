using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("FileSecureLevel")]
    public partial class FileSecureLevel
    {
        public FileSecureLevel()
        {
            FullFileInfo = new HashSet<FullFileInfo>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string SecureLevelName { get; set; }

        public virtual ICollection<FullFileInfo> FullFileInfo { get; set; }
    }
}
