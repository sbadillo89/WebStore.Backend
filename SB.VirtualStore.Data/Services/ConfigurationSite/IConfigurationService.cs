using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public interface IConfigurationService
    {
        IEnumerable<ConfigurationSite> GetAll();

        ConfigurationSite GetById(Guid id);

        void Update(ConfigurationSite configuration);

        void Create(ConfigurationSite configuration);

        bool SaveChanges();
    }
}
