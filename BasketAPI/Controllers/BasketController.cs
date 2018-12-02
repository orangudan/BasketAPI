using BasketAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketQuery _baskets;
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketQuery baskets, IBasketRepository basketRepository)
        {
            _baskets = baskets;
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var basket = _baskets.FindById(id);

            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        [HttpPost]
        public ActionResult Post()
        {
            return Ok(_basketRepository.Add());
        }
    }
}
