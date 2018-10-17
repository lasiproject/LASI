using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LASI.WebService.Data.Models;

namespace LASI.WebService.Data
{
    [Produces("application/json")]
    [Route("api/UploadDocuments")]
    public class UploadDocumentsController : Controller
    {
        private readonly DocumentsContext _context;

        public UploadDocumentsController(DocumentsContext context)
        {
            _context = context;
        }

        // GET: api/UploadDocuments
        [HttpGet]
        public IEnumerable<UploadDocument> GetUploadDocument()
        {
            return _context.Uploads;
        }

        // GET: api/UploadDocuments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUploadDocument([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uploadDocument = await _context.Uploads.SingleOrDefaultAsync(m => m.Id == id);

            if (uploadDocument == null)
            {
                return NotFound();
            }

            return Ok(uploadDocument);
        }

        // PUT: api/UploadDocuments/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UploadDocument>> PutUploadDocument([FromRoute] int id, [FromBody] UploadDocument uploadDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != uploadDocument.Id)
            {
                return BadRequest();
            }

            _context.Entry(uploadDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UploadDocumentExists(id))
            {
                return NotFound();
            }


            return NoContent();
        }

        // POST: api/UploadDocuments
        [HttpPost]
        public async Task<IActionResult> PostUploadDocument([FromBody] UploadDocument uploadDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Uploads.Add(uploadDocument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUploadDocument", new { id = uploadDocument.Id }, uploadDocument);
        }

        // DELETE: api/UploadDocuments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUploadDocument([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var uploadDocument = await _context.Uploads.SingleOrDefaultAsync(m => m.Id == id);
            if (uploadDocument == null)
            {
                return NotFound();
            }

            _context.Uploads.Remove(uploadDocument);
            await _context.SaveChangesAsync();

            return Ok(uploadDocument);
        }

        private bool UploadDocumentExists(int id)
        {
            return _context.Uploads.Any(e => e.Id == id);
        }
    }
}