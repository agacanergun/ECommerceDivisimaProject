using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Divisima.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IRepository<Brand> repoBrand;

        public HomeController(IRepository<Brand> _repoBrand)
        {
            repoBrand = _repoBrand;
        }


        //[HttpGet]
        //public string GetDate()
        //{
        //    return DateTime.Now.ToString();
        //}

        [HttpGet]
        public IEnumerable<Brand> GetBrands()
        {
            return repoBrand.GetAll().OrderByDescending(o => o.Id);
        }

        [HttpGet("{id}")]
        public Brand GetBrand(int id)
        {
            return repoBrand.GetBy(x => x.Id == id);
        }
        /// <summary>
        /// Burası Marka Ekleme 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Add(Brand brand)
        {
            try
            {
                await repoBrand.Add(brand);
                return brand.Name + " Başarıyla eklendi..";
            }
            catch (Exception ex)
            {
                return $"Marka eklenemedi hata mesajı : {ex.Message} ";
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            try
            {
                var brand = repoBrand.GetBy(x => x.Id == id);
                await repoBrand.Delete(brand);
                return brand.Name + " Başarıyla silindi..";
            }
            catch (Exception ex)
            {
                return $"Marka silinemedi hata mesajı : {ex.Message} ";
                throw;
            }
        }


        [HttpPut]
        public async Task<string> Update(Brand brand)
        {
            try
            {
                await repoBrand.Update(brand);
                return brand.Name + " Başarıyla güncellendi..";
            }
            catch (Exception ex)
            {
                return $"Marka güncellenemedi hata mesajı : {ex.Message} ";
                throw;
            }
        }

    }
}
