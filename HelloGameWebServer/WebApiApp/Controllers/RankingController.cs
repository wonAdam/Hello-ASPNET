using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedData.Models;
using System.Collections.Generic;
using System.Linq;
using WebApiApp.Data;

namespace WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        ApplicationDbContext _context;

        public RankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<GameResult> GetGameResults()
        {
            List<GameResult> result = _context.GameResults
                .OrderByDescending(item => item.Score)
                .ToList();

            return result;
        }

        [HttpGet("{id}")]
        public GameResult GetGameResult(int id)
        {
            GameResult result = _context.GameResults
                .Where(item => item.Id == id)
                .FirstOrDefault();

            return result;
        }

        [HttpPost]
        public GameResult AddGameResult([FromBody] GameResult gameResult)
        {
            _context.GameResults.Add(gameResult);
            _context.SaveChanges();
            return gameResult;
        }

        [HttpPut]
        public bool UpdateGameResult([FromBody] GameResult gameResult)
        {
            GameResult foundResult = _context.GameResults
                .Where(item => item.Id == gameResult.Id)
                .FirstOrDefault();

            if (foundResult != null)
            {
                foundResult.UserName = gameResult.UserName;
                foundResult.UserId = gameResult.UserId;
                foundResult.Date = gameResult.Date;
                foundResult.Score = gameResult.Score;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete("{id}")]
        public bool DeleteGameResult(int id)
        {
            GameResult foundResult = _context.GameResults
                .Where(item => item.Id == id)
                .FirstOrDefault();

            if (foundResult != null)
            {
                _context.Remove(foundResult);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
