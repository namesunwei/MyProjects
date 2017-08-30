using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MinioServices.Models.Response;
using MinioServices.Services;
using Snowflake;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinioServices.Controllers
{
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin"), Produces("application/json")]
    public class FilesController : Controller
    {
        private readonly MinioService _minioService;
        private readonly IdWorker _idWorker;
        public FilesController(MinioService minioService)
        {
            _minioService = minioService;
            _idWorker = new IdWorker(1, 1);
        }
    }
}
