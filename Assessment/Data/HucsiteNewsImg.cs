using System;
using System.Collections.Generic;

namespace Assessment.Data;

/// <summary>
/// TRIAL
/// </summary>
public partial class HucsiteNewsImg
{
    /// <summary>
    /// TRIAL
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// TRIAL
    /// </summary>
    public string Photo { get; set; } = null!;

    /// <summary>
    /// TRIAL
    /// </summary>
    public int? NewId { get; set; }

    /// <summary>
    /// TRIAL
    /// </summary>
    public string? Trial443 { get; set; }

    public virtual HucsiteNews? New { get; set; }
}
