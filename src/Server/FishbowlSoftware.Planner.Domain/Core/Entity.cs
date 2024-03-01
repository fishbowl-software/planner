﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FishbowlSoftware.Planner.Domain.Core;

public abstract class Entity : IEntity<string>, IDomainEventHolder
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [NotMapped, JsonIgnore]
    public List<IDomainEvent> DomainEvents { get; } = [];
}
