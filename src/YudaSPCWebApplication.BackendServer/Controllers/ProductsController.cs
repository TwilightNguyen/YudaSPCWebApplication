using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class ProductsController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Url: /api/products
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (String.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Product name cannot be empty.");
            }

            var area = await _context.ProductionAreas.FirstOrDefaultAsync(a => a.IntID == request.AreaId);

            if (area == null) {
                return BadRequest("Invalid Production Area.");
            }

            var inspPlan = await _context.InspectionPlans
                .FirstOrDefaultAsync(i => i.IntAreaID == request.AreaId && 
                i.IntID == request.InspPlanId && 
                i.BoolDeleted == false);

            if (inspPlan == null)
            {
                return BadRequest("Invalid Inspection Plan.");
            }

            var existsPro = await _context.Products
                .FirstOrDefaultAsync(p => p.StrNameProduct == request.Name && 
                p.IntAreaID == request.AreaId &&
                p.BoolDeleted == false);

            if (existsPro != null) {
                return BadRequest("Product with the same name already exists in this Production Area.");
            }

            var product = new ProductName
            {
                IntAreaID = request.AreaId,
                IntInspPlanID = request.InspPlanId,
                StrNameProduct = request.Name,
                StrModelInternal = request.ModelInternal,
                IntMoldQty = request.MoldQty,
                IntCavityQty = request.CavityQty,
                StrDescription = request.Description,
                StrNotes = request.Notes,
                StrCustomerName = request.CustomerName,
            };

            _context.Products.Add(product); 

            var result = await _context.SaveChangesAsync();

            if (result > 0) {
                return CreatedAtAction(
                    nameof(GetById),
                    new { Id =  request.InspPlanId},
                    new ProductVm
                    {
                        Id = product.IntID,
                        AreaId = product.IntAreaID,
                        Name = product.StrNameProduct,
                        Notes = product.StrNotes,
                        Description = product.StrDescription,
                        InspPlanId = product.IntInspPlanID,
                        ModelInternal = product.StrModelInternal,
                        CustomerName = product.StrCustomerName,
                        MoldQty = product.IntMoldQty,
                        CavityQty = product.IntCavityQty,
                    });
            }
            else
            {
                return BadRequest("Failed to create Product.");
            }

        }

        /// <summary>
        /// Url: /api/products/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = _context.Products.Where(r => r.BoolDeleted == false).ToList();

            if (products == null) return NotFound("No products found.");

            var productVms = products.Select(product => new ProductVm
            {
                Id = product.IntID,
                Name = product.StrNameProduct,
                AreaId = product.IntAreaID,
                InspPlanId = product.IntInspPlanID,
                ModelInternal = product.StrModelInternal,
                CustomerName = product.StrCustomerName,
                Notes = product.StrNotes,
                MoldQty = product.IntMoldQty,
                CavityQty = product.IntCavityQty,
                Description = product.StrDescription,
            });

            return Ok(productVms);
        }

        /// <summary
        /// Url: /api/product/GetById
        /// </summary>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var product = _context.Products.FirstOrDefault(p => p.IntID == Id && p.BoolDeleted == false);

            if (product == null) return NotFound("Product not found.");

            var productVm = new ProductVm
            {
                Id = product.IntID,
                Name = product.StrNameProduct,
                AreaId = product.IntAreaID,
                InspPlanId = product.IntInspPlanID,
                ModelInternal = product.StrModelInternal,
                CustomerName = product.StrCustomerName,
                Notes = product.StrNotes,
                Description = product.StrDescription,
                MoldQty = product.IntMoldQty,
                CavityQty = product.IntCavityQty,
            };

            return Ok(productVm);
        }

        /// <summary>
        /// Url: /api/products/GetByAreaId
        /// </summary>
        /// 
        [HttpGet("GetByAreaId/{AreaId:int}")]
        public async Task<IActionResult> GetByAreaId(int AreaId) {
            var products = _context.Products.Where(x => x.BoolDeleted == false && x.IntAreaID == AreaId);
            if (products == null) return NotFound("No product found for the specified production area.");

            var productVms = products?.Select(product => new ProductVm
            {
                Id = product.IntID,
                Name = product.StrNameProduct,
                AreaId = product.IntAreaID,
                InspPlanId = product.IntInspPlanID,
                CustomerName = product.StrCustomerName,
                Notes = product.StrNotes,
                Description = product.StrDescription,
                ModelInternal = product.StrModelInternal,
                MoldQty = product.IntMoldQty,
                CavityQty = product.IntCavityQty,
            });

            return Ok(productVms);
        }

        /// <summary>
        ///  Url: /api/products/pagging/
        /// </summary>
        /// 
        [HttpGet("pagging")]
        public async Task<IActionResult> Pagging(string? filter, int pageIndex, int pageSize) {
            var query = _context.Products.Where(p => p.BoolDeleted == false).AsQueryable();

            if(!string.IsNullOrEmpty(filter))
            {
                query = query.Where(p => p.StrNameProduct!.Contains(filter));
            }

            List<ProductVm> items = [..query.Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .Select(product => new ProductVm{
                    Id = product.IntID,
                    Name = product.StrNameProduct,
                    AreaId = product.IntAreaID,
                    InspPlanId= product.IntInspPlanID,
                    ModelInternal = product.StrModelInternal,
                    CustomerName = product.StrCustomerName,
                    Notes = product.StrNotes,
                    Description = product.StrDescription,
                    CavityQty= product.IntCavityQty,
                    MoldQty = product.IntMoldQty,
                })];

            var pagination = new Pagination<ProductVm>{ 
                Items = items,
                TotalRecords = query.Count()
            };

            return Ok(pagination);
        }

        /// <summary>
        /// Url:/api/products
        /// </summary>
        /// 
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int Id, ProductVm productVm) {
            if (ModelState.IsValid) { 
                return BadRequest(ModelState);
            }

            if (Id < 0 || Id != productVm.Id)
            {
                return BadRequest("Invalid product Id.");
            }

            var product = _context.Products.FirstOrDefault(x => x.IntID == Id && x.BoolDeleted == false);

            if (product == null) { 
                return NotFound("Product not found.");
            }

            if (string.IsNullOrEmpty(productVm.Name)) {
                return BadRequest("Product Name cannot be empty.");
            }

            var productExists = _context.Products.FirstOrDefault(x => 
                x.StrNameProduct == productVm.Name && 
                x.IntAreaID == product.IntAreaID &&
                x.BoolDeleted == false
            );

            if (productExists != null) {
                return BadRequest("Product with the same name already exists in production area.");
            }

            product.StrNameProduct = productVm.Name;
            product.StrDescription = productVm.Description;
            product.StrModelInternal = productVm.ModelInternal;
            product.StrNotes = productVm.Notes;
            product.StrCustomerName = productVm.CustomerName;
            product.IntMoldQty = productVm.MoldQty;
            product.IntCavityQty = productVm.CavityQty;
            
            _context.Products.Update(product);
            
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return Ok(new ProductVm
                {
                    Id = product.IntID,
                    AreaId = product.IntAreaID,
                    InspPlanId = product.IntInspPlanID,
                    ModelInternal = product.StrModelInternal,
                    MoldQty = product.IntMoldQty,
                    CavityQty = product.IntCavityQty,
                    Notes = product.StrNotes,
                    CustomerName = product.StrCustomerName,
                    Description = product.StrDescription,
                });
            }
            else
            {
                return BadRequest("Failed to update product.");
            }
        }

        /// <summary>
        /// Url: /api/products
        /// </summary>
        /// 
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteProduct(int Id) { 
            var product = _context.Products.FirstOrDefault(x => x.IntID == Id && x.BoolDeleted == false);

            if(product == null)
            {
                return NotFound("Product not found.");
            }

            product.BoolDeleted = true;

            _context.Products.Update(product);
            var result = await _context.SaveChangesAsync();

            if (result > 0) {
                return Ok(new ProductVm
                {
                    Id = product.IntID,
                    AreaId = product.IntAreaID,
                    InspPlanId= product.IntInspPlanID,
                    ModelInternal = product.StrModelInternal,
                    MoldQty = product.IntMoldQty,
                    CavityQty = product.IntCavityQty,
                    Notes = product.StrNotes,
                    CustomerName = product.StrCustomerName,
                    Description = product.StrDescription,
                });
            }
            else
            {
                return BadRequest("Failed to delete product.");
            }
        }
    }
}
