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
    public class EditModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public EditModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        //当对 Movies/Edit 页面进行 HTTP GET 请求时（例如 http://localhost:5000/Movies/Edit/2）：
        //OnGetAsync 方法从数据库提取电影并返回 Page 方法。
        //Page 方法呈现“Pages/Movies/Edit.cshtml”Razor 页面。 
        //Pages/Movies/Edit.cshtml 文件包含模型指令(@model RazorPagesMovie.Pages.Movies.EditModel)，这使电影模型在页面上可用。 
        //“编辑”表单中会显示电影的值。
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //当发布 Movies/Edit 页面时：此页面上的表单值将绑定到 Movie 属性。 [BindProperty] 特性会启用模型绑定。
        //如果模型状态中存在错误（例如，ReleaseDate 无法被转换为日期），则会使用已提交的值重新显示表单。
        //如果没有模型错误，则电影已保存。
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Movie).State = EntityState.Modified;

            //当一个客户端删除电影并且另一个客户端对电影发布更改时，代码会检测并发异常。
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
