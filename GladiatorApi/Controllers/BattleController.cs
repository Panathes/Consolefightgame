﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using GladiatorApi.Dto;
using GladiatorLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GladiatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        private readonly IGame _game; 

        public BattleController(IGame game)
        {
            _game = game;
        }

        [HttpPost]
        public Task<IActionResult> Create()
        {         
            Guid battleId = _game.StartBattle();         

            IActionResult ret = Ok(battleId);
            Task<IActionResult> test = Task.FromResult(ret);
            return test;

        }

        [HttpGet("list")]
        public IActionResult ListBattle()
        {
            List<Guid> battleList = _game.ListBattle();
            return Ok(battleList);
        }

        [HttpPost("{id}/register")]
        public Task<IActionResult> RegisterPlayerInBattle(Guid id, [FromBody] GameRegisterPlayerRequestDto request)
        {

            Guid gladiatorId = _game.RegisterPlayerInBattle(id, request.GladiatorName);

            IActionResult test = Ok(gladiatorId);
            Task<IActionResult> test1 = Task.FromResult(test);
            return test1;
        }

        [HttpPost("{id}/action")]
        public Task<IActionResult> ChooseAction(Guid id, [FromBody] PlayerChooseActionRequestDto request)
        {
            _game.ChooseAction(request.PlayerId, request.Action, id);

            IActionResult ok = Ok();
            Task<IActionResult> nothingToSend = Task.FromResult(ok);
            return nothingToSend;
        }

        [HttpGet("{id}/playerlist")]
        public IActionResult ShowPlayerInBattle(Guid id)
        {

            List<Gladiator> playerNumber = _game.ShowPlayerInBattle(id);
            List<GladiatorDto> response = new List<GladiatorDto>();

            for (int i = 0; i < playerNumber.Count; i++)
            {
               var toto = new GladiatorDto();
               toto.Name = playerNumber[i].Name;
               toto.Pv = playerNumber[i].Pv;
               toto.Stamina = playerNumber[i].Stamina;
               toto.GladiatorId = playerNumber[i].GladiatorId;
               response.Add(toto);
            }

            return Ok(response);
//            return Ok(playerNumber);
        }

        [HttpPost("{id}/fight")]
        public Task<IActionResult> RunBattle(Guid id)
        {
            
            bool runBattle = _game.RunBattle(id);
            string winner = _game.FinishBattle(id);

            List<Gladiator> playerNumber = _game.ShowPlayerInBattle(id);
            List<PlayerInfoDto> response = new List<PlayerInfoDto>();

            if (runBattle) 
            {              
//                var battleFinish = new WinnerInfoDto();
//                battleFinish.Stillfighting = true;
//                battleFinish.Winner = winner;
//
//                IActionResult okItsFinish = Ok(battleFinish);
//                Task<IActionResult> sendYouTheWinner = Task.FromResult(okItsFinish);
//                return sendYouTheWinner;
            }
       
            for (int i = 0; i < playerNumber.Count; i++)
            {
                var toto = new PlayerInfoDto();
                toto.Name = playerNumber[i].Name;
                toto.Pv = playerNumber[i].Pv;
                toto.Stamina = playerNumber[i].Stamina;
                toto.PlayerId = playerNumber[i].GladiatorId;
                toto.Stillfighting = runBattle;
                toto.Winner = winner;
                response.Add(toto);
            }

            IActionResult ok = Ok(response);
            Task<IActionResult> nothingToSend = Task.FromResult(ok);
            return nothingToSend;

        }

    }
}