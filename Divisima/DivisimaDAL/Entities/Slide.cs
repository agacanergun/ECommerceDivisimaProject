using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    [Table("Slide")]
    public class Slide
    {
        public int Id { get; set; }

        [StringLength(50), Column(TypeName = "varchar(50)"), Required(ErrorMessage = "Slayt Adı boş geçilemez...")]
        public string Name { get; set; }

        [StringLength(50), Column(TypeName = "varchar(50)"),Display(Name="Slayt başlığı")]
        public string Title { get; set; }

        [StringLength(250), Column(TypeName = "varchar(250)"), Display(Name = "Slayt açıklaması")]
        public string Description { get; set; }

        [StringLength(150), Column(TypeName = "varchar(150)"), Display(Name = "Slayt resmi")]
        public string Pıcture { get; set; }

        [StringLength(150), Column(TypeName = "varchar(150)"), Display(Name = "bağlantı linki")]
        public string Link { get; set; }

        [Display(Name = "Görüntülenme sayısı")]
        public int DisplayIndex { get; set; }
    }
}
