using System;
using ArStore.API.Models.API;
using ArStore.API.Tools;
using Microsoft.AspNetCore.Mvc;

namespace ArStore.API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController] 
    public class OrdersController:ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromForm]OrderApiRequest request)
        {
            try
            {
                Validator.Validate(request);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentNullException e)
            {    
                return BadRequest(e.Message);
            }

            try
            {
               // OrderService.AddOrder(request);
                //var recipient = _configuration["Email:Smtp:RecipientEmail"];
                //_emailService.Send("lipov758@gmail.com", "????? ????? ?? ?????????? ??????????", "? ??? ????? ?????");
                return Ok();
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }      
        }
    }
}