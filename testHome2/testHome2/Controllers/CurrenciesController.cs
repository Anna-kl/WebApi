
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TestHomeWork.Models;

namespace testHome2.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class CurrenciesController : ControllerBase
        {
           

            private readonly EFDataContext _context;

            public CurrenciesController(EFDataContext context)
            {
                _context= context;
            }

            [HttpGet("{year}",Name = "GetAllData")]
            public async Task<List<Currency>> Get(int year)
            {
            var currensies = _context.Currencies
                .AsNoTracking();
            return await currensies
                .Where(_ => _.Date >= new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .Where(_ => _.Date < new DateTime(year + 1, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .Take(10)
                .ToListAsync();
            }

            [HttpGet("get-name", Name = "GetCurrencyNames")]
            public async Task<List<string>> GetName()
            {
                return await _context.Currencies
                 .AsNoTracking()
                 .Select(_ => _.CurrencyName)
                 .Distinct()
                 .ToListAsync();
            }

        [HttpGet("get-currencies-date", Name = "GetCurrencyForDate")]
        public async Task<List<Currency>> GetCurrencyForDate([FromQuery] string date,
            [FromQuery] string name)
        {
            DateTime? dateRequest = null;
            if (date != null)
            {
                dateRequest = DateTime.Parse(date);
                DateTime.SpecifyKind(dateRequest.Value, DateTimeKind.Local);
            }
          
            return await _context.Currencies
             .AsNoTracking()
             .Where(_ => dateRequest != null ? _.Date.Date == dateRequest.Value.Date : true)
             .Where(_ => name != null ? _.CurrencyName == name : true)
             .ToListAsync();
        }
       

    }
}
