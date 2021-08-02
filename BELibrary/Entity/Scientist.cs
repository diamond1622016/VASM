using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BELibrary.Entity
{
    [Table("Scientist")]
    public partial class Scientist
    {
        public Guid Id { get; set; }
        [StringLength(250)]
        public string Scholar_id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public string Interests { get; set; }

        [StringLength(250)]
        public string Url_picture { get; set; }
        [StringLength(500)]
        public string Affiliation { get; set; }
        // New Field
        [StringLength(250)]
        public string Academic_pos { get; set; }
        [StringLength(250)]
        public string Qualification { get; set; }
        [StringLength(250)]
        public string Position { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        public string AFF_Desc { get; set; }
        public string Cites_per_year { get; set; }
        [StringLength(250)]
        public string Citedby { get; set; }
        [StringLength(250)]
        public string I10Index { get; set; }
        [StringLength(250)]
        public string HIndex { get; set; }
        [StringLength(250)]
        public string Hindex5y { get; set; }
        [StringLength(250)]
        public string I10index5y { get; set; }
        [StringLength(250)]
        public string Citedby5y { get; set; }
        [StringLength(250)]
        public string Paper_num { get; set; }
        [StringLength(250)]
        public string Email_domain { get; set; }
        [StringLength(250)]
        public string Nationality { get; set; }
        public string Coauthors { get; set; }
        public string Bio_sketch { get; set; }
        public string Working_plan { get; set; }
        public string Interested_topics{ get; set; }
        [StringLength(250)]
        public string Personal_web_link { get; set; }
        public string PaperList { get; set; }
        [StringLength(5)]
        public string Type { get; set; }
        public Guid? OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

    }
}