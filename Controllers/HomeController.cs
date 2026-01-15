using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using 测试C.Models;

namespace 测试C.Controllers;

public class HomeController : Controller
{
    // 假数据存储
    private static List<(int Id, string Name)> _data = new List<(int, string)>
    {
        (1, "Item 1"),
        (2, "Item 2"),
        (3, "Item 3")
    };

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // 获取所有数据
    [HttpGet]
    public JsonResult GetAll()
    {
        return Json(_data);
    }

    // 创建新数据
    [HttpPost]
    public JsonResult Create([FromBody] string name)
    {
        var newId = _data.Any() ? _data.Max(d => d.Id) + 1 : 1;
        _data.Add((newId, name));
        return Json(new { Success = true, Id = newId });
    }

    // 编辑数据
    [HttpPost]
    public JsonResult Edit([FromBody] (int Id, string Name) item)
    {
        var existingItem = _data.FirstOrDefault(d => d.Id == item.Id);
        if (existingItem != default)
        {
            _data.Remove(existingItem);
            _data.Add(item);
            return Json(new { Success = true });
        }
        return Json(new { Success = false, Message = "Item not found" });
    }

    // 删除数据
    [HttpPost]
    public JsonResult Delete([FromBody] int id)
    {
        var item = _data.FirstOrDefault(d => d.Id == id);
        if (item != default)
        {
            _data.Remove(item);
            return Json(new { Success = true });
        }
        return Json(new { Success = false, Message = "Item not found" });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
