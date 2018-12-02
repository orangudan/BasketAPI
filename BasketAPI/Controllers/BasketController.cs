using Microsoft.AspNetCore.Mvc;
using System;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private Func<Basket> _basketAdder;

        public BasketController(Func<Basket> basketAdder)
        {
            _basketAdder = basketAdder;
        }

        public ActionResult Post()
        {
            var newBasket = _basketAdder();
            return Ok(newBasket);
        }
    }

    public class Basket
    {
        public Guid Id { get; set; }
    }
}
