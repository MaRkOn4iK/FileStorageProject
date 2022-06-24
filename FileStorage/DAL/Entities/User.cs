using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            FullFileInfo = new HashSet<FullFileInfo>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string Login { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public virtual ICollection<FullFileInfo> FullFileInfo { get; set; }
    }
}
