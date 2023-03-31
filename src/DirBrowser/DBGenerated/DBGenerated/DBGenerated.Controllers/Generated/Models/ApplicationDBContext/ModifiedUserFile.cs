﻿//this was autogenerated by a tool. Do not modify! Use partial
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Generated;
[ApiController]
[Route("api/[controller]")]    
public partial class REST_ApplicationDBContext_ModifiedUserFileController : Controller
{
    private ApplicationDBContext _context;
    public REST_ApplicationDBContext_ModifiedUserFileController(ApplicationDBContext context)
	{
        _context=context;
	}
    [HttpGet]
    public async Task<ModifiedUserFile_Table[]> Get(){
        var data= await _context.ModifiedUserFile.ToArrayAsync();
        var ret = data.Select(it => (ModifiedUserFile_Table)it!).ToArray();
        return ret;

        
    }
    
        [HttpGet("{id}")]
    public async Task<ActionResult<ModifiedUserFile_Table>> GetModifiedUserFile(long id)
    {
        if (_context.ModifiedUserFile == null)
        {
            return NotFound();
        }
        var item = await _context.ModifiedUserFile.FirstOrDefaultAsync(e => e.ID==id);

        if (item == null)
        {
            return NotFound();
        }

        return (ModifiedUserFile_Table)item!;
    }


    [HttpPatch("{id}")]
        public async Task<IActionResult> PutModifiedUserFile(long id, ModifiedUserFile value)
        {
            if (id != value.ID)
            {
                return BadRequest();
            }

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModifiedUserFileExists(id))
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

        [HttpPost]
        public async Task<ActionResult<ModifiedUserFile>> PostModifiedUserFile(ModifiedUserFile_Table value)
        {
          
            var val = new ModifiedUserFile();
            val.CopyFrom(value);
            _context.ModifiedUserFile.Add(val);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModifiedUserFile", new { id = val.ID }, val);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModifiedUserFile(long id)
        {
            if (_context.ModifiedUserFile == null)
            {
                return NotFound();
            }
            var item = await _context.ModifiedUserFile.FirstOrDefaultAsync(e => e.ID==id);
            if (item == null)
            {
                return NotFound();
            }

            _context.ModifiedUserFile .Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModifiedUserFileExists(long id)
        {
            return (_context.ModifiedUserFile.Any(e => e.ID  == id));
        }

    }    
