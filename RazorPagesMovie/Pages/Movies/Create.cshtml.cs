using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public CreateModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
            //OnGet 方法初始化页面所需的任何状态。 
            //“创建”页没有任何要初始化的状态，因此返回 Page。 
            //在本教程的后面部分中，将介绍 OnGet 初始化状态的示例。 
            //Page 方法创建用于呈现 Create.cshtml 页的 PageResult 对象。
        }

        //Movie 属性使用 [BindProperty] 特性来选择加入模型绑定。 
        //当“创建”表单发布表单值时，ASP.NET Core 运行时将发布的值绑定到 Movie 模型。
        [BindProperty]
        public Movie Movie { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //当页面发布表单数据时，运行 OnPostAsync 方法
        public async Task<IActionResult> OnPostAsync()
        {
            //如果存在任何模型错误，将重新显示表单，以及发布的任何表单数据。
            //在发布表单前，可以在客户端捕获到大部分模型错误。 模型错误的一个示例是，发布的日期字段值无法转换为日期
            if (!ModelState.IsValid)
            {
                return Page();//存在错误，返回Create页
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();
        
            return RedirectToPage("./Index");// 如果不存在模型错误，将保存数据，并且浏览器会重定向到索引页（/Movies/Index）。
        }
       
    }
}
