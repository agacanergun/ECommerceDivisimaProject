using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public class Institutional
    {
        public int Id { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"), Required(ErrorMessage = "Başlık Boş Geçilemez"), Display(Name = "Başlık")]
        public string Title { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(ErrorMessage = "Link Boş Geçilemez"), Display(Name = "Link")]
        public string Link { get; set; }
    }
}
