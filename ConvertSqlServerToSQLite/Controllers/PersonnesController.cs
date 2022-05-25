using ConvertSqlServerToSQLite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ConvertSqlServerToSQLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnesController : ControllerBase
    {
        private readonly sqldbContext _context;
        private readonly liteContext _litecontext;
        private bool disposedValue;

        public PersonnesController(sqldbContext context, liteContext liteContext)
        {
            _context = context;
            _litecontext = liteContext;
        }

        //// GET: api/Personnes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonnes()
        {
            return await _context.Personnes.ToListAsync();
        }

        // GET: api/Personnes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personne>> GetPersonne(int id)
        {
            var personne = await _context.Personnes.FindAsync(id);


            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        // PUT: api/Personnes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonne(int id, Personne personne)
        {
            if (id != personne.Id)
            {
                return BadRequest();
            }

            _context.Entry(personne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Personnes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonne(int id)
        {
            var personne = await _context.Personnes.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }

            _context.Personnes.Remove(personne);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonneExists(int id)
        {
            return _context.Personnes.Any(e => e.Id == id);
        }

        //public void Dispose()
        //{
        //    _context.Dispose();
        //    GC.SuppressFinalize(_context);

        //    _litecontext.Dispose();
        //    GC.SuppressFinalize(_litecontext);   
        //}

        //POST: api/Personnes
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> ConverteSQLServerToSQLite(Personne personne)
        {
            var dbname = "myDb.db";
            string path = Environment.CurrentDirectory + "/" + dbname;

            await _litecontext.Database.EnsureDeletedAsync();
            await _litecontext.Database.EnsureCreatedAsync();
            //await _litecontext.Database.MigrateAsync();

            List<Personne> p = await _context.Personnes.ToListAsync();
            await _litecontext.AddRangeAsync(p);
            await _litecontext.SaveChangesAsync();


            await _context.Database.CloseConnectionAsync();
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);

            await _litecontext.Database.CloseConnectionAsync();
            await _litecontext.DisposeAsync();
            GC.SuppressFinalize(this);

            //var fileBytes = Encoding.UTF8.GetBytes(path);
            //MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(dbname));
            //System.IO.File.WriteAllBytes(ndb, ms.ToArray());

            //var nDB = dbname.Clone();
            //MemoryStream ms = new MemoryStream();
            //FileStream file = new FileStream(nDB.ToString(), FileMode.Create, FileAccess.Write, FileShare.Read);
            //ms.WriteTo(file);
            //file.Close();
            //ms.Close();

            //using (FileStream file = new FileStream(dbname, FileMode.Open, FileAccess.Read)) 
            //dbname.CopyTo(ms);

            var fileBytes = System.IO.File.Open(dbname, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //var fileBytes = System.IO.File.ReadAllBytes(dbname);
            return File(fileBytes, "application/octet-stream", dbname);
                
        }

    }
}
