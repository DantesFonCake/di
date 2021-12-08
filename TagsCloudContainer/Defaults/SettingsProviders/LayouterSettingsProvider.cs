﻿using Autofac;
using Mono.Options;
using System.Drawing;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Registrations;

namespace TagsCloudContainer.Defaults.SettingsProviders;

public class LayouterSettingsProvider : ICliSettingsProvider
{
    public Point Center { get; set; } = new Point(300, 100);

    public OptionSet GetCliOptions()
    {
        var options = new OptionSet()
        {
            {"center=", $"Coordinates of center for CircularLayouter, separated by ','. Defaults to {Center}", v => Center = Parse(v)}
        };

        return options;
    }

    private static Point Parse(string v)
    {
        var coords = v.Split(',');
        if (coords.Length != 2)
            throw new FormatException($"String {v} was in incorrect format, should be two ints separated by ','");
        return new Point(int.Parse(coords[0]), int.Parse(coords[1]));
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<LayouterSettingsProvider>().AsSelf().As<ICliSettingsProvider>().SingleInstance();
    }
}