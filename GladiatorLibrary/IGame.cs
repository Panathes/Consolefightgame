﻿using System;
using System.Collections.Generic;

namespace GladiatorLibrary
{
    public interface IGame
    {
        void ChooseAction(Guid playerId, PlayerActions action, Guid battleId);
        Guid StartBattle(string name);
        Guid RegisterPlayerInBattle(Guid battleId, String name);
        bool RunBattle(Guid battleId);
        string FinishBattle(Guid battleId);
        List<Guid> ListBattle();
        List<Gladiator> ShowPlayerInBattle(Guid battleId);
        Gladiator ShowPlayerWinner(Guid battleId);
    }
}