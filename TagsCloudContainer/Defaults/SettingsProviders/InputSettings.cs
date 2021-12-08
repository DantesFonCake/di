﻿using Autofac;
using Mono.Options;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class InputSettings : IRequiredSettingsProvider
{
    public string[] Paths { get; private set; } = Array.Empty<string>();

    public string Source { get; private set; } = string.Empty;

    public bool IsSet { get; private set; }

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
            {
                {"files=", $"Specifies input files separated by '|'.", v =>
                    {
                        Paths = v.Split('|');
                        IsSet=true;
                    }
                },
                {"string=", $"Specifies string to read.", v =>
                    {
                        Source = v;
                        IsSet=true;
                    }
                }
            };

        return options;
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<InputSettings>().AsSelf().As<ICliSettingsProvider>().SingleInstance();
    }
}