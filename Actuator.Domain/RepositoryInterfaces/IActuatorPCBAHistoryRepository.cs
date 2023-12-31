﻿using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IActuatorPCBAHistoryRepository
{
    Task<List<ActuatorPCBAChange>> GetPCBAChangesForActuator(int woNo, int serialNo);
    Task PCBARemoved(ActuatorPCBAChange change);
}