using CodeTest.Data;
using CodeTest.Models;
using CodeTest.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CodeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayloadsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public PayloadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody] PayloadViewModel dto)
        {
            try
            {
                var g = JsonSerializer.Serialize(dto.Data);
                var payload = new Payload
                {
                    DeviceId = dto.DeviceId,
                    DeviceType = dto.DeviceType,
                    DeviceName = dto.DeviceName,
                    GroupId = dto.GroupId,
                    DataType = dto.DataType,
                    Data = dto.Data != null ? JsonSerializer.Serialize(dto.Data) : null,
                    Timestamp = dto.Timestamp
                };
                _context.Payloads.Add(payload);
                await _context.SaveChangesAsync();
                return Ok("Data saved successfully");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPayloads()
        {
            try
            {
                var payloads = await _context.Payloads.ToListAsync();

                var payloadDtos = new List<PayloadViewModel>();
                foreach (var payload in payloads)
                {
                    var data = payload.Data != null ? JsonSerializer.Deserialize<object>(payload.Data) : null;

                    var payloadDto = new PayloadViewModel
                    {
                        DeviceId = payload.DeviceId,
                        DeviceType = payload.DeviceType,
                        DeviceName = payload.DeviceName,
                        GroupId = payload.GroupId,
                        DataType = payload.DataType,
                        Data = data,
                        Timestamp = payload.Timestamp
                    };

                    payloadDtos.Add(payloadDto);
                }

                return Ok(payloadDtos);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
