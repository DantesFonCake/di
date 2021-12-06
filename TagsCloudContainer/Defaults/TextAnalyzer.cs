﻿using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class TextAnalyzer : ITextAnalyzer
{
    private readonly ITextReader textReader;
    private readonly IWordNormalizer[] wordNormalizers;
    private readonly IWordFilter[] wordFilters;
    private readonly char[] wordSeparators;

    public TextAnalyzer(ITextReader textReader, IWordNormalizer[] wordNormalizers, IWordFilter[] wordFilters, TextAnalyzerSettings settings)
    {
        this.textReader = textReader;
        this.wordNormalizers = wordNormalizers;
        this.wordFilters = wordFilters;
        wordSeparators = settings.WordSeparators;
    }

    public ITextStats AnalyzeText()
    {
        var result = new TextStats();
        foreach (var line in textReader.ReadLines())
        {
            var words = line
                .Split(wordSeparators)
                .Where(x => !string.IsNullOrWhiteSpace(x));
            foreach (var word in ApplyNormalizingAndFiltering(words))
            {
                result.UpdateWord(word);
            }
        }

        return result;
    }

    private IEnumerable<string> ApplyNormalizingAndFiltering(IEnumerable<string> words)
    {
        foreach (var normalizer in wordNormalizers)
        {
            words = words.Select(normalizer.Normalize);
        }

        foreach (var filter in wordFilters)
        {
            words = words.Where(filter.IsValid);
        }

        return words;
    }
}
