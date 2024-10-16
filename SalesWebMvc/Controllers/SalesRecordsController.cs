using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesWebMvc.Models;
using SalesWebMvc.Services;


namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;
        private readonly SellerService _sellerService;

        public SalesRecordsController(SalesRecordService salesRecordService, SellerService sellerService)
        {
            _salesRecordService = salesRecordService;
            _sellerService = sellerService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _salesRecordService.FindAllAsync(); // Supondo que você tenha esse método
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["SellerId"] = new SelectList(await _sellerService.FindAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesRecord salesRecord)
        {
            // Ignoramos a validação do objeto Seller no ModelState
            ModelState.Remove("Seller");

            if (ModelState.IsValid)
            {
                // Buscar o vendedor pelo SellerId e associá-lo ao SalesRecord
                salesRecord.Seller = await _sellerService.FindByIdAsync(salesRecord.SellerId);

                if (salesRecord.Seller != null) // Verifica se o vendedor existe
                {
                    await _salesRecordService.InsertAsync(salesRecord);
                    return RedirectToAction(nameof(Index)); // Redireciona para a lista de vendas
                }
                ModelState.AddModelError("SellerId", "Vendedor inválido."); // Adiciona erro se vendedor não for encontrado
            }

            // Se o ModelState não for válido, ou se o vendedor não existir, retornar à view de criação
            ViewData["SellerId"] = new SelectList(await _sellerService.FindAllAsync(), "Id", "Name", salesRecord.SellerId);
            return View(salesRecord);
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}