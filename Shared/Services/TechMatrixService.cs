namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class TechMatrixService
{
    private enum EnumCaller
    {
        None,
        Effects,
        Units,
        Villagers
    }
    public BasicList<CustomTechModel> AllTechs { get; private set; } = [];
    private CustomTechModel? _current;
    public void AddVillagers(int villagersToSpawn)
    {
        // Ensure at least one villager is spawned
        if (villagersToSpawn <= 0)
        {
            throw new CustomBasicException("Must spawn at least one villager");
        }
        Validate(EnumCaller.Villagers);
        _current!.VillagersToSpawn = villagersToSpawn;
    }
    private void Validate(EnumCaller caller)
    {
        if (_current == null)
        {
            throw new CustomBasicException("Must start first.");
        }
        if (caller == EnumCaller.Effects)
        {
            //effects is calling this.
            if (_current.Units.Count > 0)
            {
                throw new CustomBasicException("There was already units so can't add effects");
            }
            if (_current.VillagersToSpawn > 0)
            {
                throw new CustomBasicException("There was already villagers to spawn so can't add effects");
            }
            return;
        }
        if (_current.Effects.Count > 0)
        {
            if (caller == EnumCaller.Units)
            {
                throw new CustomBasicException("Cannot add units because you had other effects");
            }
            else
            {
                throw new CustomBasicException("Cannot spawn villagers because you had other effects");
            }
        }
        if (caller == EnumCaller.Units)
        {
            if (_current.VillagersToSpawn > 0)
            {
                throw new CustomBasicException("You cannot add units because you spawned villagers");
            }
            return;
        }
        if (caller == EnumCaller.Villagers)
        {
            if (_current.Units.Count > 0)
            {
                throw new CustomBasicException("You cannot spawn villagers because you already spawned custom units");
            }
            return;
        }
        throw new CustomBasicException("Wrong caller");
    }
    public void AddCustomUnit(string unitName, int count)
    {
        Validate(EnumCaller.Units);
       
        if (_current!.Effects.Count > 0)
        {
            throw new CustomBasicException("You already added effects.");
        }
        
        if (_current.ActivationType is EnumActivationType.LimitedTime or EnumActivationType.Timespan)
        {
            throw new CustomBasicException("Cannot add custom units with LimitedTime or Timespan activations.");
        }

        if (_current.RecipientType != EnumRecipentType.Human)
        {
            throw new CustomBasicException("Custom units are only allowed for Human techs.");
        }

        CustomUnitModel model = new()
        {
            ProtoName = unitName,
            HowMany = count
        };

        _current.Units.Add(model);
    }

    public void AddEffect(BasicEffectModel effect)
    {
        Validate(EnumCaller.Effects);

        _current!.Effects.Add(effect);
    }
    public void AddSeveralEffects(BasicList<BasicEffectModel> effects)
    {
        Validate(EnumCaller.Effects);
        _current!.Effects.AddRange(effects);
    }
    private void StartActivation(EnumRecipentType recipientType, EnumActivationType activationType, int time = 0, int startTime = 0, int endTime = 0)
    {
        _current = new CustomTechModel()
        {
            RecipientType = recipientType,
            ActivationType = activationType,
            Time = time,
            StartTime = startTime,
            EndTime = endTime
        };
        if (activationType == EnumActivationType.Timespan && startTime > endTime)
        {
            throw new CustomBasicException("Start time cannot be later than end time for Timespan activations.");
        }
        if (activationType == EnumActivationType.LimitedTime && time < 1)
        {
            throw new CustomBasicException("LimitedTime activations must last at least 1 second.");
        }
        if (activationType == EnumActivationType.Delayed && time < 1)
        {
            throw new CustomBasicException("Delayed activations must be delayed at least 1 second.");
        }
        AllTechs.Add(_current);
    }
    //so a person can do extras if they want.
    private void AddPrerequisiteToCurrent(BasicPrereqModel prereqType)
    {
        if (_current == null)
        {
            throw new CustomBasicException("Activation has not been started yet.");
        }

        // Dynamically add the prerequisite to the current activation
        _current.Prereqs.Add(prereqType);
    }
    // Helper method to handle prerequisites
    public void AddPrerequisite(BasicPrereqModel prereq)
    {
        if (_current == null)
        {
            throw new CustomBasicException("Activation has not been started yet.");
        }

        // Determine the correct recipient type and add the prerequisite accordingly
        if (_current.RecipientType == EnumRecipentType.Human || _current.RecipientType == EnumRecipentType.Computer || _current.RecipientType == EnumRecipentType.GlobalObtainable)
        {
            if (prereq is SpecificAgeModel agePrereq)
            {
                AddPrerequisiteToCurrent(agePrereq);
            }
            else if (prereq is TechStatusActiveModel techPrereq)
            {
                AddPrerequisiteToCurrent(techPrereq);
            }
            else if (prereq is TypeCountModel unitPrereq)
            {
                AddPrerequisiteToCurrent(unitPrereq);
            }
            else
            {
                throw new CustomBasicException("Unknown prerequisite type.");
            }
        }
        else
        {
            throw new CustomBasicException("Invalid recipient type for prerequisite.");
        }
    }

    public void AddSeveralPrerequisitesToCurrent(BasicList<BasicPrereqModel> prereqs)
    {
        if (_current == null)
        {
            throw new CustomBasicException("Activation has not been started yet.");
        }
        _current.Prereqs.AddRange(prereqs);
    }

    // Universal method to start activations
    public void StartActivationForRecipient(EnumRecipentType recipientType, EnumActivationType activationType, int time = 0, int startTime = 0, int endTime = 0)
    {
        if (activationType == EnumActivationType.LimitedTime)
        {
            if (recipientType == EnumRecipentType.GlobalObtainable)
            {
                throw new CustomBasicException("Global techs cannot have LimitedTime activations.");
            }
        }

        StartActivation(recipientType, activationType, time, startTime, endTime);
    }

    // This can now be used for any recipient (Human, Computer, GlobalSharedRMS)
    public void StartHumanForeverActivation() => StartActivationForRecipient(EnumRecipentType.Human, EnumActivationType.Forever);
    public void StartComputerForeverActivation() => StartActivationForRecipient(EnumRecipentType.Computer, EnumActivationType.Forever);
    public void StartGlobalForeverActivation() => StartActivationForRecipient(EnumRecipentType.GlobalObtainable, EnumActivationType.Forever);

    // Time-based Activations
    public void StartHumanLimitedActivation(int time) => StartActivationForRecipient(EnumRecipentType.Human, EnumActivationType.LimitedTime, time);
    public void StartComputerLimitedActivation(int time) => StartActivationForRecipient(EnumRecipentType.Computer, EnumActivationType.LimitedTime, time);
    public void StartGlobalSharedLimitedActivation(int time) => StartActivationForRecipient(EnumRecipentType.GlobalSharedRMS, EnumActivationType.LimitedTime, time);

    public void StartHumanDelayedActivation(int time) => StartActivationForRecipient(EnumRecipentType.Human, EnumActivationType.Delayed, time);
    public void StartComputerDelayedActivation(int time) => StartActivationForRecipient(EnumRecipentType.Computer, EnumActivationType.Delayed, time);
    public void StartGlobalSharedDelayedActivation(int time) => StartActivationForRecipient(EnumRecipentType.GlobalSharedRMS, EnumActivationType.Delayed, time);

    public void StartHumanTimeSpanActivation(int start, int end) => StartActivationForRecipient(EnumRecipentType.Human, EnumActivationType.Timespan, 0, start, end);
    public void StartComputerTimeSpanActivation(int start, int end) => StartActivationForRecipient(EnumRecipentType.Computer, EnumActivationType.Timespan, 0, start, end);
    public void StartGlobalSharedTimeSpanActivation(int start, int end) => StartActivationForRecipient(EnumRecipentType.GlobalSharedRMS, EnumActivationType.Timespan, 0, start, end);
    // Prerequisite-based Activations (Including GlobalSharedRMS)
    // Activation Methods (Including GlobalSharedRMS)
    public void StartHumanTechRequirement(string techName)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.Human,
            ActivationType = EnumActivationType.Forever
        };

        AddPrerequisite(new TechStatusActiveModel(techName));

        AllTechs.Add(_current);
    }

    public void StartComputerTechRequirement(string techName)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.Computer,
            ActivationType = EnumActivationType.Forever
        };

        AddPrerequisite(new TechStatusActiveModel(techName));

        AllTechs.Add(_current);
    }

    public void StartGlobalTechRequirement(string techName)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.GlobalObtainable,
            ActivationType = EnumActivationType.Forever
        };

        AddPrerequisite(new TechStatusActiveModel(techName));

        AllTechs.Add(_current);
    }

    public void StartHumanAgeRequirementActivation(int age)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.Human,
            ActivationType = EnumActivationType.Forever
        };

        AddPrerequisite(new SpecificAgeModel(age));

        AllTechs.Add(_current);
    }

    public void StartComputerAgeRequirementActivation(int age)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.Computer,
            ActivationType = EnumActivationType.Forever
        };

        AddPrerequisite(new SpecificAgeModel(age));

        AllTechs.Add(_current);
    }

    public void StartGlobalAgeRequirementActivation(int age)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.GlobalObtainable,
            ActivationType = EnumActivationType.Forever
        };

        AddPrerequisite(new SpecificAgeModel(age));

        AllTechs.Add(_current);
    }

    public void StartHumanSimpleUnitRequirement(string unit, int count)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.Human,
            ActivationType = EnumActivationType.Forever
        };

        AddPrerequisite(new TypeCountModel()
        {
            Operator = oo1.GreaterThanEqual,
            Unit = unit,
            State = "aliveState",
            Count = count
        });

        AllTechs.Add(_current);
    }

    public void StartComputerSimpleUnitRequirement(string unit, int count)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.Computer,
            ActivationType = EnumActivationType.Forever
        };
        AddPrerequisite(new TypeCountModel()
        {
            Operator = oo1.GreaterThanEqual,
            Unit = unit,
            State = "aliveState",
            Count = count
        });

        AllTechs.Add(_current);
    }
    public void StartGlobalSimpleUnitRequirement(string unit, int count)
    {
        _current = new CustomTechModel()
        {
            RecipientType = EnumRecipentType.GlobalObtainable,
            ActivationType = EnumActivationType.Forever
        };
        AddPrerequisite(new TypeCountModel()
        {
            Operator = oo1.GreaterThanEqual,
            Unit = unit,
            State = "aliveState",
            Count = count
        });
        AllTechs.Add(_current);
    }
}