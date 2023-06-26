﻿using TheDashboard.BuildingBlocks.Core.EventBus;

namespace TheDashboard.DataConsumerService.Infrastructure.Integration;

public record DashboardCreatedEvent(Guid Id, string Name) : IntegrationEvent;

public record DashboardDeletedEvent(Guid Id) : IntegrationEvent;

public record DashboardUpdatedEvent(Guid Id, string Name) : IntegrationEvent;


// InsertOrUpdate
