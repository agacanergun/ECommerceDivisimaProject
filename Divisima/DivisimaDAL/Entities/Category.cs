using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisima.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Üst Kategori")]
        public int? ParentID { get; set; }
        public Category ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(ErrorMessage = "Kategori Adı Boş Geçilemez"), Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        public int DisplayIndex { get; set; }

    }
}
