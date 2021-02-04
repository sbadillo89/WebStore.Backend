using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public class ConfigurationService : IConfigurationService
    {

        private readonly AppDbContext _context;

        public ConfigurationService(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<ConfigurationSite> GetAll()
        {
            return _context.ConfigurationSites.ToList();
        }

        public ConfigurationSite GetById(Guid id)
        {
            return _context.ConfigurationSites.Where(x=> x.Active).FirstOrDefault(r => r.Id == id);
        }

        public void Create(ConfigurationSite configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //Se inactivan las configuraciones anteriores y solo se deja activa la actual.
            InactiveAllRecords();

            _context.ConfigurationSites.Add(configuration);
        }
        public void Update(ConfigurationSite configuration)
        {
            _context.Entry(configuration).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        public bool SaveChanges()
        {
            return Convert.ToBoolean(_context.SaveChanges());
        }

        #region "Methods privates"

        private void InactiveAllRecords()
        {
            _context.ConfigurationSites.ToList().ForEach(x => x.Active = false);
        }
        #endregion "Methods privates"
    }
}
