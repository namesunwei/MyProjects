using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Chris.Blog.Models;
using Chris.Blog.Models.RequestModels;
using Chris.Blog.Models.ViewModels;
using Chris.Blog.Services;
using Chris.Blog.Services.DomainModels;

namespace Chris.Blog.Controllers
{
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        private readonly ArticlesService _articlesService;
        public ArticleController(ArticlesService articlesService)
        {
            _articlesService = articlesService;
        }

        #region 展示博文
        [HttpGet("Index/{id}")]
        public async Task<IActionResult> Index(long id)
        {
            var result = await _articlesService.QueryByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }
            var viewModel = Mapper.Map<ArticleViewModel>(result);

            return View(viewModel);
        }
        #endregion

        #region 新增博文
        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddConfirme(ArticleRequest req)
        {
            var domainModel = Mapper.Map<ArticleDomain>(req);

            var articleId = await _articlesService.AddAsync(domainModel);

            return Json(new { data = Url.Action("Index", new { id = articleId }) });
        }
        #endregion

        #region 修改博文
         //todo:修改博文

        #endregion

        #region 删除博文
        //todo:删除功能
        #endregion
    }
}
