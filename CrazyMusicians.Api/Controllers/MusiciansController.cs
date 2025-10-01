using CrazyMusicians.Api.Dtos;
using CrazyMusicians.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusiciansController : ControllerBase
    {
        // Basit In-Memory liste (ödev için yeterli)
        private static readonly List<Musician> _data =
        [
            new() { Id=1,  Name="Ahmet Çalgı",   Profession="Ünlü Çalgı Çalar",       FunTrait="Her zaman yanlış nota çalar, ama çok eğlenceli" },
            new() { Id=2,  Name="Zeynep Melodi", Profession="Popüler Melodi Yazar",   FunTrait="Şarkıları yanlış anlaşılır ama çok popüler" },
            new() { Id=3,  Name="Cemil Akor",    Profession="Çılgın Akorist",         FunTrait="Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
            new() { Id=4,  Name="Fatma Nota",    Profession="Sürpriz Nota Üreticisi", FunTrait="Nota üretirken sürekli sürprizler hazırlar" },
            new() { Id=5,  Name="Hasan Ritim",   Profession="Ritim Canavarı",         FunTrait="Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
            new() { Id=6,  Name="Elif Armoni",   Profession="Armoni Ustası",          FunTrait="Armonilerini bazen yanlış çalar, ama çok yaratıcıdır" },
            new() { Id=7,  Name="Ali Perde",     Profession="Perde Uygulayıcı",       FunTrait="Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
            new() { Id=8,  Name="Ayşe Rezonans", Profession="Rezonans Uzmanı",        FunTrait="Rezonans uzmanı; bazen gürültü çıkarır" },
            new() { Id=9,  Name="Murat Ton",     Profession="Tonlama Meraklısı",      FunTrait="Farklı tonlamalar komik ama ilginç" },
            new() { Id=10, Name="Selin Akor",    Profession="Akor Sihirbazı",         FunTrait="Akor değiştirince sihirli hava yaratır" }
        ];

        
        [HttpGet]
        public ActionResult<IEnumerable<Musician>> GetAll(
            [FromQuery] string? search,  // [FromQuery] istenen
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            // Arama + basit sayfalama
            IEnumerable<Musician> q = _data;
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.ToLower();
                q = q.Where(m =>
                    m.Name.ToLower().Contains(s) ||
                    m.Profession.ToLower().Contains(s) ||
                    m.FunTrait.ToLower().Contains(s));
            }

            var total = q.Count();
            var items = q.OrderBy(m => m.Id)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();

            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        // GET: /api/musicians/3
        [HttpGet("{id:int}")]
        public ActionResult<Musician> GetById(int id)
        {
            var m = _data.FirstOrDefault(x => x.Id == id);
            return m is null ? NotFound() : Ok(m);
        }

        // POST: /api/musicians
        [HttpPost]
        public ActionResult<Musician> Create([FromBody] MusicianCreateDto dto)
        {
            var newId = _data.Count == 0 ? 1 : _data.Max(x => x.Id) + 1;
            var m = new Musician
            {
                Id = newId,
                Name = dto.Name.Trim(),
                Profession = dto.Profession.Trim(),
                FunTrait = dto.FunTrait.Trim()
            };
            _data.Add(m);
            return CreatedAtAction(nameof(GetById), new { id = m.Id }, m);
        }

        // PUT: /api/musicians/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] MusicianUpdateDto dto)
        {
            var m = _data.FirstOrDefault(x => x.Id == id);
            if (m is null) return NotFound();

            m.Name = dto.Name.Trim();
            m.Profession = dto.Profession.Trim();
            m.FunTrait = dto.FunTrait.Trim();
            return NoContent();
        }

        // PATCH: /api/musicians/5
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<MusicianPatchDto> patch)
        {
            var m = _data.FirstOrDefault(x => x.Id == id);
            if (m is null) return NotFound();

            var dto = new MusicianPatchDto { Name = m.Name, Profession = m.Profession, FunTrait = m.FunTrait };
            patch.ApplyTo(dto, ModelState);          // ModelState ile doğrula
            if (!TryValidateModel(dto)) return ValidationProblem(ModelState);

            if (dto.Name != null) m.Name = dto.Name.Trim();
            if (dto.Profession != null) m.Profession = dto.Profession.Trim();
            if (dto.FunTrait != null) m.FunTrait = dto.FunTrait.Trim();
            return NoContent();
        }

        // DELETE: /api/musicians/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var m = _data.FirstOrDefault(x => x.Id == id);
            if (m is null) return NotFound();
            _data.Remove(m);
            return NoContent();
        }
    }
}
