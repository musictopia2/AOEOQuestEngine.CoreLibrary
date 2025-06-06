﻿namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface ITrainableUnitHandler
{
    abstract static string SupportedProtoName { get; }
    bool FromDock => false;
    void Populate(XElement source, TrainableUnitModel unit, ILocalizedStringService localizer);
}