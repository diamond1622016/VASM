using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELibrary.Entity
{
    [Table("Paper")]
    public partial class Paper
    {
        public Guid Id { get; set; }
        [StringLength(20)]
        public string Author_pub_id { get; set; }
        public string Title { get; set; }
        [StringLength(20)]
        public string Pub_year { get; set; }
        public string Authors { get; set; }
        [StringLength(500)]
        public string Journal { get; set; }
        // New Field
        [StringLength(20)]
        public string Volume { get; set; }
        [StringLength(20)]
        public string Number { get; set; }
        [StringLength(20)]
        public string Pages { get; set; }
        [StringLength(250)]
        public string Publisher { get; set; }
        public string Note { get; set; }
        public string Cites_per_year { get; set; }
        [StringLength(20)]
        public string Num_citations { get; set; }
        public string Paper_link { get; set; }
        public Guid? ScientistId { get; set; }
        public virtual Scientist Scientist { get; set; }

    }
}
