using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileData;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace DNP2API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamiliesController: ControllerBase
    {
        public FileContext FileContext;

        public FamiliesController(FileContext FileContext)
        {
            this.FileContext = FileContext;
        }
        [HttpGet]
        public async Task<ActionResult<IList<Family>>>
            GetFamilies([FromQuery] int? Id, [FromQuery] string? filter)
        {
            try
            {
                IList<Family> families = await FileContext.GetFamilies();
                if (Id != null)
                {
                    families =  families.Where(t => t.Id == Id).ToList();
                }

                if (filter != null)
                {
                    families = families.Where(t =>
                    {
                        string names = "";
                        foreach (var a in t.Adults)
                        {
                            names += a.FirstName.ToLower() + a.LastName.ToLower();
                        }
                        return names.Contains(filter.ToLower());
                    }).ToList();
                }
                return Ok(families);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Family>> AddFamily([FromBody] Family family)
        {
            try
            {
                Family added = await FileContext.AddFamilyAsync(family);
                return Created($"{added.Id}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteFamily([FromRoute] int id) {
            try {
                await FileContext.RemoveFamilyAsync(id);
                return Ok();
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Family>> UpdateFamily([FromBody] Family family) {
            try {
                Family updatedFamily = await FileContext.UpdateAsync(family);
                return Ok(updatedFamily); 
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}