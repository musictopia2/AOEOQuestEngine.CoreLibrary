using System;
using System.Collections.Generic;
using System.Text;

namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;

public interface IEffectRemovalFilter
{
    /// <summary>
    /// Returns true if the effect should be removed, false to keep it.
    /// </summary>
    bool ShouldRemove(BasicEffectModel effect);
}