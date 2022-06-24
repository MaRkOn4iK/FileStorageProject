using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("FileType")]
    public partial class FileType
    {
        public FileType()
        {
            File = new HashSet<File>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string TypeName { get; set; }

        public virtual ICollection<File> File { get; set; }
    }
}
