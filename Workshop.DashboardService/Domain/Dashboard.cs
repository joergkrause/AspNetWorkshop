﻿using System.ComponentModel.DataAnnotations;

namespace Workshop.Domain;

public class Dashboard : EntityBase<Guid>
{
  [Required]
  [StringLength(100, MinimumLength = 3)]
  public string Name { get; set; } = default!;

  public string Theme { get; set; }

  public int Version { get; set; }

}
