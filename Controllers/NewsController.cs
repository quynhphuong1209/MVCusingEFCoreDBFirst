using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCusingEFCoreDBFirst.Models;
using MVCusingEFCoreDBFirst.Models.Domain;
using PagedList;

namespace MVCusingEFCoreDBFirst.Controllers
{

    public class NewsController : Controller
    {
        private readonly QuynhPhuongContext _context;
       
        public NewsController(QuynhPhuongContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index(int? page = 0)
        {
            float limit = 10;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)((page - 1) * limit);

            ViewBag.pageCurrent = page;

            float totalNews = _context.News.Count(p=>(p.IsDeleted==false || p.IsDeleted==null));

            ViewBag.totalNews = totalNews;

            float numberpage = totalNews / limit;

            ViewBag.numberPage = (int)Math.Ceiling(numberpage);

            var data = _context.News.Include(n => n.Type).Skip(start).Take((int)limit);

            return View(data);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            var lstType = _context.NewsTypes.Where(p => p.IsDeleted == false && p.IsShow == true).Select(p => new SelectListItem
            {
                Value=p.Id.ToString(),
                Text=p.Name
            }).ToList();
            ViewBag.Type = lstType;
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(News news, IFormFile file)
        {
            //if (ModelState.IsValid)
            //{
                if(file !=null)
                {
                    string uploadsFolder = Path.Combine("Upload", "Images");
                    news.ImageUrl =new Random().Next(0,1000000).ToString()+ file.FileName;
                    string filePath = Path.Combine(uploadsFolder, news.ImageUrl);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                news.Date = DateTime.Now;
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["TypeId"] = new SelectList(_context.NewsTypes, "Id", "Id", news.TypeId);
            //return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.NewsTypes, "Id", "Name", news.TypeId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Date,Author,ImageUrl,TypeId,Summary,IsDeleted,IsShow")] News news, IFormFile file)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            if (file != null)
            {
                string uploadsFolder = Path.Combine("Upload", "Images");
                news.ImageUrl = new Random().Next(0, 1000000).ToString() + file.FileName;
                string filePath = Path.Combine(uploadsFolder, news.ImageUrl);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["TypeId"] = new SelectList(_context.NewsTypes, "Id", "Id", news.TypeId);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        //public ActionResult Index(string search, int? page)
        //{
        //    var pageNumber = page ?? 1;
        //    var pageSize = 10; //Show 10 rows every time
        //    var brands = this._UoW.BrandsRepository.GetAll().Where(b =>
        //                 b.BrandName.Contains(search) ||
        //                 search == null).ToPagedList(pageNumber, pageSize);
        //    return View(brands);
        //}



    }
}
