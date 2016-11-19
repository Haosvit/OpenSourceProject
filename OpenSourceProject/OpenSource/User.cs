namespace OpenSourceProject.OpenSource
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Verifications = new HashSet<Verification>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string PersonId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(15)]
        public string Gender { get; set; }

        public virtual ICollection<Verification> Verifications { get; set; }
    }
}
