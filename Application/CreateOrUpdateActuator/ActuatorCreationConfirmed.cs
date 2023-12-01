using BuildingBlocks.Domain;
using Domain.Entities;
using Domain.Events;
using Domain.Repositories;
using LINTest.LinakDB;
using LINTest.Models;

namespace Application.CreateOrUpdateActuator;

internal sealed class ActuatorCreationConfirmedDomainEventHandler : IDomainEventListener<ActuatorCreatedDomainEvent>
{
    private readonly IPCBARepository _pcbaRepository;
    private readonly IPCBAService _pcbadao;

    public ActuatorCreationConfirmedDomainEventHandler(IPCBARepository pcbaRepository, IPCBAService pcbadao)
    {
        _pcbaRepository = pcbaRepository;
        _pcbadao = pcbadao;
    }

    public async Task Handle(ActuatorCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
    }

    private PCBA ToDomain(PCBAModel pcbaModel)
    {
        return new PCBA(
            uid: pcbaModel.Uid.ToString(), 
            manufacturerNo: pcbaModel.ManufacturerNumber, 
            itemNumber: pcbaModel.ItemNumber, 
            software: pcbaModel.Software, 
            productionDateCode: pcbaModel.ProductionDateCode);
    }
}