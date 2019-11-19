using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        //[BindProperty] 会绑定名称与属性相同的表单值和查询字符串
        //在 GET 请求中进行绑定需要 (SupportsGet = true)
        //For security reasons, you must opt in to binding GET request data to page model properties.
        [BindProperty(SupportsGet =true)]
        //包含用户在搜索文本框中输入的文本
        public string SearchString { get; set; }

        /// <summary>
        /// 包含流派列表。 Genres 使用户能够从列表中选择一种流派。
        /// </summary>
        
        [BindProperty]
        public SelectList Genres { get; set; }

        /// <summary>
        /// 包含用户选择的特定流派（例如“西部”）
        /// </summary>
        [BindProperty(SupportsGet =true)]
        public string MovieGenre { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie select m;
            if(!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString));
            }

            if(!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            //流派的 SelectList 是通过投影不包含重复值的流派创建的
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
