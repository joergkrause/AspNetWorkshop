﻿using Workshop.BuildingBlocks.Core.EventBus;

namespace Workshop.UiInfoService.Infrastructure.Integration.Events;

public record DashboardCreatedEvent(Guid Id, string Name) : IntegrationEvent;

public record DashboardDeletedEvent(Guid Id) : IntegrationEvent;

public record DashboardUpdatedEvent(Guid Id, string Name) : IntegrationEvent;


// InsertOrUpdate
