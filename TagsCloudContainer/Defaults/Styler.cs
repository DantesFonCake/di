﻿using Autofac;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class Styler : IStyler
{
    private readonly StyleProvider settings;

    public Styler(StyleProvider settings)
    {
        this.settings = settings;
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<Styler>().AsSelf().As<IStyler>();
    }

    public IStyledTag Style(ITag source)
    {
        var style = settings.GetStyle(source);
        return new StyledTag(source.Value, style);
    }
}
