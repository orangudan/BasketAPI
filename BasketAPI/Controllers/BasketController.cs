using BasketAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var basket = _basketRepository.FindById(id);

            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        [HttpPost]
        public ActionResult Post()
        {
            return Ok(_basketRepository.Add());
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var basket = _basketRepository.FindById(id);

            if (basket == null)
                return NotFound();

            _basketRepository.Remove(basket);

            return NoContent();
        }
    }
}
