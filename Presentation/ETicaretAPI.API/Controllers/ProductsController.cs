using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IWebHostEnvironment _webHostEnvironment;

        readonly private IFileService _fileService;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Paginator paginator)
        {
            await Task.Delay(1000);
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(paginator.Page * paginator.Size).Take(paginator.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            });

            return Ok(new
            {
                totalCount,
                products
            });
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(string Id)
        {

            return Ok(await _productReadRepository.GetByIdAsync(Id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Product_Create product_Create)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = product_Create.Name,
                Stock = product_Create.Stock,
                Price = product_Create.Price
            });
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Product_Update model)
        {
            Product p = await _productReadRepository.GetByIdAsync(model.Id);
            p.Name = model.Name;
            p.Stock = model.Stock;
            p.Price = model.Price;
            await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveById(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {

                await _fileService.UploadAsync("resource/product-images",Request.Form.Files); // Request.Form.Files

            return Ok();
        }
    }
}
