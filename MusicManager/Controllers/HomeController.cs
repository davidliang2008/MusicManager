using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicManager.Models;

namespace MusicManager.Controllers
{
    public class HomeController : Controller
    {
        MusicManagerEntities db = new MusicManagerEntities();
        //
        // GET: /Home/
        public ActionResult Index(string sortOrder, string filterString)
        {
            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "Date DESC";
            var albums = from a in db.Albums
                         select a;
            // SQL return 0 row when passing empty string but string.Contains return
            // all rows
            if (!string.IsNullOrEmpty(filterString))
            {
                ViewBag.filterString = filterString;
                albums = albums.Where(a => a.Genre.ToUpper() == filterString.ToUpper());
            }
            switch (sortOrder)
            {
                case "Date":
                    albums = albums.OrderBy(a => a.CreateTime);
                    break;
                case "Date DESC":
                    albums = albums.OrderByDescending(a => a.CreateTime);
                    break;
                case "Title":
                    albums = albums.OrderBy(a => a.Title);
                    break;
                case "Title DESC":
                    albums = albums.OrderByDescending(a => a.Title);
                    break;
                case "Artist":
                    albums = albums.OrderBy(a => a.Artist);
                    break;
                case "Artist DESC":
                    albums = albums.OrderByDescending(a => a.Artist);
                    break;
                case "Genre":
                    albums = albums.OrderBy(a => a.Genre);
                    break;
                case "Genre DESC":
                    albums = albums.OrderByDescending(a => a.Genre);
                    break;
                default:
                    albums = albums.OrderByDescending(a => a.CreateTime);
                    break;
            }
            ViewBag.sortParm = sortOrder;
            return View(albums.ToList());
        }
        //
        // GET: /Home/Create
        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Home/Create
        [HttpPost]
        public JsonResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                album.CreateTime = System.DateTime.Now;
                db.Albums.Add(album);
                foreach (Track track in album.Tracks)
                {
                    track.AlbumId = album.AlbumId;
                    db.Tracks.Add(track);
                }
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        //
        // GET: /Home/Edit/1
        public ActionResult Edit(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }
        //
        // POST: /Home/Edit/1
        [HttpPost]
        public JsonResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                // remove all original tracks
                var tracks = (from t in db.Tracks
                              where t.AlbumId == album.AlbumId
                              select t).ToList();
                foreach (var track in tracks)
                {
                    db.Tracks.Remove(track);
                }
                // insert new track objects
                foreach (var new_track in album.Tracks)
                {
                    db.Tracks.Add(new_track);
                }
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        //
        // GET: /Home/Delete/1
        public ActionResult Delete(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }
        //
        // POST: /Home/Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
