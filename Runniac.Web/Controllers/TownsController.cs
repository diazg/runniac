using AutoMapper;
using log4net;
using Runniac.Business;
using Runniac.Data;
using Runniac.ExternalDataExtraction;
using Runniac.Utils;
using Runniac.Web.ViewModels;
using Runniac.Web.WebUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace Runniac.Web.Controllers
{
    public class TownsController : ApiController
    {
        private ITownService _townsService;

        public TownsController(ITownService townService)
        {
            _townsService = townService;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public IEnumerable<string> Search(string query)
        {
            var towns = _townsService.Search(query);
            return towns;
        }

        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public TownVM GetByName(string name)
        {
            return Mapper.Map<Town, TownVM>(_townsService.GetByName(name));
        }
    }
}