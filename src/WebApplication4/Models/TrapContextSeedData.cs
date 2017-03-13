using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class TrapContextSeedData
    {
        private TrapContext _context;

        public TrapContextSeedData(TrapContext context)
        {
            _context = context;
        }

        public void EsureSeedData()
        {
            //Dodawnianie danych
            if(!_context.Traps.Any())
            {
                var pulapka = new Trap()
                {
                    UserName = "mikze",
                    Battery = 3,
                    CreateDate = DateTime.UtcNow,
                    LastMadified = DateTime.Today,
                    FirstRat = DateTime.Today,
                    Rats = 2,
                    Latitude = 2.3,
                    Longitude = 32.4
                };

                _context.Traps.Add(pulapka);
                _context.SaveChanges();
            }

        }
    }
}
