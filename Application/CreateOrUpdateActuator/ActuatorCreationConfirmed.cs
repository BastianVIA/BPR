using BuildingBlocks.Domain;
using Domain.Entities;
using Domain.Events;
using Domain.Repositories;
using LINTest.LinakDB;
using LINTest.Models;

namespace Application.CreateOrUpdateActuator;

internal sealed class ActuatorCreationConfirmedDomainEventHandler : IDomainEventListener<ActuatorCreatedDomainEvent>
{
    private readonly IPCBARepository pcbaRepository;
    private readonly IPCBADAO pcbadao;

    public ActuatorCreationConfirmedDomainEventHandler(IPCBARepository pcbaRepository, IPCBADAO pcbadao)
    {
        this.pcbaRepository = pcbaRepository;
        this.pcbadao = pcbadao;
    }

    public async Task Handle(ActuatorCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var pcba = pcbadao.GetPCBA(notification.PCBAUid);
        await pcbaRepository.UpdatePCBA(ToDomain(pcba));
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