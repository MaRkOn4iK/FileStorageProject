using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("File")]
    public partial class File
    {
        public File()
        {
            FullFileInfo = new HashSet<FullFileInfo>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string FileName { get; set; }

        public byte[] FileStreamCol { get; set; }

        public DateTime? FileCreateDate { get; set; }

        public int? FileTypeId { get; set; }

        public virtual FileType FileType { get; set; }

        public virtual ICollection<FullFileInfo> FullFileInfo { get; set; }
    }
}
